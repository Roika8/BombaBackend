﻿using BLL.HistorytPortfolio.Portfolios;
using BLL.MainPortfolio;
using BLL.Services;
using BLL.TrackingPortfolioHandler.Portfolios;
using BombaRestAPI.DTO;
using BombaRestAPI.DTOs;
using DATA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BombaRestAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseApiController
    {
        readonly ILogger<UsersController> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;
        private readonly UserManager<User> _userMananger;

        public UsersController(ILogger<UsersController> logger, SignInManager<User> signInManager, TokenService tokenService)
        {
            _logger = logger;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userMananger = signInManager.UserManager;
        }

        private UserDto CreateUserDtoObject(User user)
        {
            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = _tokenService.CreateToken(user),
                UserName = user.UserName
            };
        }

        [Route("RegisterUser")]
        [HttpPost]
        public async Task<ActionResult<UserDto>> RegisterUser(RegisterDto registerDto)
        {
            try
            {
                if (await _userMananger.Users.AnyAsync(user => user.Email == registerDto.Email))
                {
                    _logger.LogError($"Couldnt register user couse Email: '{registerDto.Email}' already exist");
                    return BadRequest("Email already exists");
                }
                if (await _userMananger.Users.AnyAsync(user => user.UserName == registerDto.UserName))
                {
                    _logger.LogError($"Couldnt register user couse User name: '{registerDto.UserName}' already taken");
                    return BadRequest("UserName already taken");
                }
                var user = new User
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Email = registerDto.Email,
                    UserName = registerDto.UserName
                };
                var result = await _userMananger.CreateAsync(user, registerDto.Password);
                if (result.Succeeded)
                {
                    await Mediator.Send(new CreatePortfolio.Command { UserID = user.Id });
                    await Mediator.Send(new CreateHistoryPortfolio.Command { UserID = user.Id });
                    await Mediator.Send(new CreateTrackingPortfolio.Command { UserID = user.Id });

                    return CreateUserDtoObject(user);
                }
                else
                {
                    StringBuilder sb = new();
                    result.Errors.ToList().ForEach(e => sb.AppendLine(e.Description));
                    return BadRequest(sb.ToString());
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }


        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            try
            {
                var foundUser = await _userMananger.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
                return CreateUserDtoObject(foundUser);


            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            try
            {
                User user = await _signInManager.UserManager.FindByEmailAsync(loginDto.Email);
                if (user == null) return Unauthorized();

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return CreateUserDtoObject(user);
                }
                return Unauthorized();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }


    }
}
