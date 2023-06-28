namespace PokedexApiCaller.Contract;

public class ErrorStatus
{
    public Status? Status { get; set; } = null;
}

public class Status
{
    public int HttpStatusCode { get; set; }
    public string Message { get; set; }
}