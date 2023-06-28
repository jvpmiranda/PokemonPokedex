using ApiPokedex.Contract;
using ApiPokedex.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiPokedex.Controllers;

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
    [HttpPost("Token")]
    public ActionResult<Authentication> Token([FromBody]string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false
            };

        var claims = handler.ValidateToken(token, validations, out var tokenSecure);
        if (claims.HasClaim("clientId", ApiInformation.ClientId.ToString()) &&
            claims.HasClaim(c => c.Type == "name"))
        {
            return Ok(CreateToken(claims.FindFirstValue("name")));
        }
        else
            return ValidationProblem("token informed is not valid");
    }

    private Authentication CreateToken(string name)
    {
        var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, "Authenticated")
            };

        var expires = DateTime.UtcNow.AddMinutes(15);

        var token = new JwtSecurityToken(
            //_jwtSettings.Issuer,
            //_jwtSettings.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: credentials);


        return new Authentication() 
        { 
            Token = new JwtSecurityTokenHandler().WriteToken(token), 
            ExpirationDate = expires 
        };
    }
}