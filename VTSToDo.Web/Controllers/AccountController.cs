using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VTSToDo.Models;
using VTSToDo.Services;

namespace VTSToDo.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IConfiguration configuration, IAuthenticationService authenticationService)
        {
            _configuration = configuration;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            var user = await _authenticationService.LoginUser(request.Username, request.Password);
            if (user == null)
            {
                var response = new Dictionary<string, string>
                {
                    { "Error", "Invalid username or password" }
                };
                return BadRequest(response);
            }

            var token = GenerateJwtToken(user.Id, user.UserName);

            return Ok(new
            {
                Access_Token = token,
                UserName = user.UserName
            });
        }

        private string GenerateJwtToken(int userId, string username)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
