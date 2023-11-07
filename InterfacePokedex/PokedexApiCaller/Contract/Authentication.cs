using PokedexApiCaller.Contract;

namespace PokedexApiCaller.Contract;

public class Authentication : ErrorStatus
{
    public string Token { get; set; }

    public DateTime ExpirationDate { get; set; }
}
