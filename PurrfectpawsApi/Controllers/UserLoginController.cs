using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PurrfectpawsApi.DatabaseDbContext;
using PurrfectpawsApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PurrfectpawsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly PurrfectpawsContext _context;

        public UserLoginController(IConfiguration config, PurrfectpawsContext context)
        {
            _configuration = config;
            _context = context;
        }

        // GET: api/UserLogin/Login
        [HttpPost("Login")]
        public async Task<ActionResult<TUserLogin>> PostUserLogin([FromBody] TUserLogin login)
        {
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            var isUserExist = await _context.TUsers.SingleOrDefaultAsync(u => u.Email == login.email);
            if (isUserExist == null)
            {
                return NotFound("Invalid user");
            }

            if (BCrypt.Net.BCrypt.Verify(login.password, isUserExist.Password))
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserId", isUserExist.UserId.ToString()),
                    new Claim("UserName", isUserExist.Name),
                    new Claim("Email", isUserExist.Email)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: signIn);
                var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
                isUserExist.Access_Token = accessToken;
                await _context.SaveChangesAsync();

                return Ok(accessToken);

            } else
            {
                return BadRequest("Invalid credentials");
            }

        }
    }
}
