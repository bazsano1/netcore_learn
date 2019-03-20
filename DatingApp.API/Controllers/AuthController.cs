using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.DTOs;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration config;

        public AuthController(IAuthRepository repository, IConfiguration config)
        {
            _authRepository = repository;
            this.config = config;
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserForLoginDto usr)
        {
            var user = await _authRepository.Login(usr.Username, usr.Password);

            if (user == null)
                return Unauthorized();

            return Ok(GetTokenObject(user));

        }

        private object GetTokenObject(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new
            {
                token = tokenHandler.WriteToken(token)
            };
        }
    }
}