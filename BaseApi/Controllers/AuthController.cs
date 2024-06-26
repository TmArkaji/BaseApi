﻿using BaseApi.Data;
using BaseApi.DTOs;
using BaseApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IApiUserRepository _apiUserRepository;
        private readonly IApiUserLoginRepository _apiUserLoginRepository;

        public AuthController(IConfiguration configuration, IApiUserRepository apiUserRepository, IApiUserLoginRepository apiUserLoginRepository)
        {
            _configuration = configuration;
            _apiUserRepository = apiUserRepository;
            _apiUserLoginRepository = apiUserLoginRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            var user = await _apiUserRepository.ValidateUserAsync(userLogin.UserName, userLogin.PasswordHash, userLogin.ApiKey, userLogin.CountryA2);

            if (user == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.ID)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Registrar el inicio de sesión
            var userLoginRecord = new ApiUserLogin
            {
                ApiUserId = user.ID,
                Token = tokenString,
                TokenStart = DateTime.UtcNow,
                TokenEnd = DateTime.UtcNow.AddHours(1)
            };

            await _apiUserLoginRepository.LogUserLoginAsync(userLoginRecord);

            return Ok(new { Token = tokenString });
        }
    }
}
