using System.Text.Json.Serialization;

namespace ApiPokedex.Contract;

public class ErrorStatus
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Status? Status { get; set; } = null;
}

public class Status
{
    public int HttpStatusCode { get; set; }
    public string Message { get; set; }
}