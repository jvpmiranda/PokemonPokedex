using Microsoft.IdentityModel.Tokens;
using PokedexApiCaller.Contract;
using PokedexApiCaller.Factory;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PokedexApiCaller;

public class AuthCallerApiCaller
{
    private readonly object _lockNewToken = new object();
    private readonly string _baseUrlApi;
    private readonly string _secret;
    private readonly int _clientId = 13031995;
    private static Authentication Auth = new Authentication();

    public AuthCallerApiCaller(string baseUrlApi, string secret)
    {
        _baseUrlApi = baseUrlApi;
        _secret = secret;
    }

    public async Task<Authentication> GetToken(string name)
    {
        lock (_lockNewToken)
        {
            if (Auth.ExpirationDate > DateTime.UtcNow.AddSeconds(15))
                return Auth;

            HttpClient client = HttpClientPokemonApiFactory.Create(_baseUrlApi);
            var token = CreateToken(name);
            HttpResponseMessage response = client.PostAsJsonAsync($"api/v1/Auth/Token", token).Result;
            Auth = response.Content.ReadAsAsync<Authentication>().Result;
        }

        return Auth;
    }

    private string CreateToken(string name)
    {
        var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
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