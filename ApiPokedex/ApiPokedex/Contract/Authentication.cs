namespace ApiPokedex.Contract;

public class Authentication
{
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
}
