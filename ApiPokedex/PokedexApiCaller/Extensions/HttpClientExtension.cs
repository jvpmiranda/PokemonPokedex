using PokedexApiCaller.Contract;

namespace System.Net.Http;

public static class HttpClientExtension
{
    public static void SetAuthentication(this HttpClient http, Authentication auth)
    {
        http.DefaultRequestHeaders.Clear();
        http.DefaultRequestHeaders.Add("Authorization", $"Bearer {auth.Token}");
    }
}
