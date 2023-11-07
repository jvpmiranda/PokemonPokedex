using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PokedexApiCaller.Config;
using PokedexApiCaller.Contract;
using PokedexApiCaller.Factory;
using PokedexApiCaller.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PokedexApiCaller.Services;

public class AuthApiCaller : IAuthApiCaller
{
    private readonly object _lockNewToken = new object();
    private readonly FactoryHttpClient _factory;
    private readonly JwtSettings _jwtSettings;
    private readonly int _clientId = 13031995;
    private Authentication Auth = new Authentication();

    public AuthApiCaller(FactoryHttpClient factory, IOptions<JwtSettings> jwtSettings)
    {
        _factory = factory;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Authentication> GetToken(string name)
    {
        lock (_lockNewToken)
        {
            if (Auth.ExpirationDate > DateTime.UtcNow.AddSeconds(15))
                return Auth;

            using HttpClient client = _factory.Create();
            var token = CreateToken(name);
            HttpResponseMessage response = client.PostAsJsonAsync($"api/v1/Auth/Token", token).Result;
            Auth = response.Content.ReadAsAsync<Authentication>().Result;
        }

        return Auth;
    }

    private string CreateToken(string name)
    {
        var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("clientId", _clientId.ToString()),
            new Claim("name", name)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}