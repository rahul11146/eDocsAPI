using eDocsAPI.Interface;
using eDocsAPI.Models;
using eDocsAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eDocsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUser _userRepository;
        private readonly IConfiguration _configuration;

        public LoginController(IUser userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpGet, Route("Authenticate")]
        public async Task<IActionResult> Authenticate(string username, string password)
        {
            var user = await _userRepository.Authenticate(username, password);

            if (user?.UserName != null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }

            return NotFound("user not found");
        }

        private string GenerateToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Role,user.RoleName)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                                            claims,
                                            expires: DateTime.Now.AddMinutes(5),
                                            signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
