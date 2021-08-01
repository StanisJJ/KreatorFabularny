using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testowy.Helpers;
using testowy.Models;
using testowy.Models.User;
using testowy.Repository;

namespace testowy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _dataRepository;
        public UserController(IUserService dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [Authorize("Admin,User")]
        [HttpGet]
        public IActionResult Get()
        {
            User us = new User();
            us.Login = "asdasd";
            IEnumerable<User> users = _dataRepository.GetAll();
            return Ok(new { users , us });
        }

        // GET: api/Employee/5
        
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            User user = _dataRepository.Get(id);
            if (user == null)
            {
                return NotFound("The User record couldn't be found.");
            }
            return Ok(user);
        }
       
        [HttpGet("Login/{name}")]
        public IActionResult FindByName(string name)
        {
            UserResponse userResponse = _dataRepository.GetByLogin(name);
            if (userResponse == null)
            {
                return NotFound("The User record couldn't be found.");
            }
            return Ok(userResponse);
        }

    }
}
