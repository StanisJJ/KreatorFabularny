using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testowy.Entities;
using testowy.Helpers;
using testowy.Models.User;
using testowy.Services;

namespace testowy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService userService)
        {
            _authService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _authService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        [HttpPost("register")]
        public IActionResult Register(UserRequest userRequest)
        {
            var response = _authService.Register(userRequest);
            if (!response) return BadRequest(new { message = "Email/Login/Password is invalid!" });
            return Ok();
        }

        [Authorize("Admin,User")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _authService.GetAll();
            return Ok(users);
        }
    }
}
