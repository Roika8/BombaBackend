using BLL.Interfaces;
using BLL.Services;
using BombaRestAPI.DTO;
using BombaRestAPI.DTOs;
using DATA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BombaRestAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;
        public UsersController(ILogger<UsersController> logger, IUserService userService, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
        {
            _logger = logger;
            _userService = userService;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [Route("RegisterUser")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser(User userData)
        {
            try
            {
                //TODO Check is exist
                bool res = await _userService.RegisterUser(userData);
                return res ? Ok() : BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }


        [Route("GetUser")]
        [HttpGet]
        public async Task<IActionResult> GetUser(Guid userID)
        {
            try
            {
                User foundUser = await _userService.GetUser(userID);
                return Ok(foundUser);
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
                    return new UserDto
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Token = _tokenService.CreateToken(user)
                    };
                }
                return Unauthorized();

                //bool foundUser = await _userService.IsUserExist(email, password);
                //return foundUser ? Ok(foundUser) : BadRequest("Invalid user email or password");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
