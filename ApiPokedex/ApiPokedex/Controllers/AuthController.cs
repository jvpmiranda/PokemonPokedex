using ApiPokedex.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiPokedex.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        public JwtSettings _jwtSettings { get; set; }

        public AuthController(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        [MapToApiVersion("1.0")]
        public ActionResult Token(string name)
        {

            var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);

            string role;
            if (name == "Joao")
                role = "admin";
            else
                role = "user";

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                //_jwtSettings.Issuer,
                //_jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);


            return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) } );
        }
    }
}