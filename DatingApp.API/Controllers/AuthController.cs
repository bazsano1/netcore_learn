using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.DTOs;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IAuthRepository _authRepository;

        public AuthController(IAuthRepository repository)
        {
            _authRepository = repository;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegistrartionDto usr)
        {
            // no need for[FromBody] input parameter attribute
            // no need to check modelstate if ApiController attribute is applied
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState)
            //}

            // TODO: validation
            usr.Username = usr.Username.ToLower();

            if (await _authRepository.UserExists(usr.Username))
            {
                return BadRequest("Username already exists!");
            }

            var user = new User
            {
                UserName = usr.Username
            };

            user = await _authRepository.Register(user, usr.Password);

            return StatusCode(201);
        }
    }
}