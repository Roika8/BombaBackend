using BLL.Interfaces;
using DATA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BombaRestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [Route("AddUser")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User userData)
        {
            try
            {
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
        [HttpGet]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                bool foundUser = await _userService.IsUserExist(email, password);
                return foundUser ? Ok(foundUser) : BadRequest("Invalid user email or password");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
