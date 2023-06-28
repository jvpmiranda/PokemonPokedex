namespace ApiPokedex.Contract.v1.Out;

public class OutGameVersion : ErrorStatus
{
    public int VersionId { get; set; }

    public string Description { get; set; }

    public string VersionName { get; set; }
}