using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CloudInvoice.Catalog.Api.Controllers
{
    // ⚠️ TEMPORÁRIO - APAGAR quando a Identity.API real estiver disponível.
    [ApiController]
    [Route("api/dev")]
    public class DevTokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DevTokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>Gera um token JWT de teste. Ex: /api/dev/token?role=Admin</summary>
        [HttpGet("token")]
        public IActionResult GenerateToken([FromQuery] string role = "User")
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Secret"]!;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, "teste@cloudinvoice.pt"),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = tokenString });
        }
    }
}