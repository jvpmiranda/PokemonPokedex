namespace SqlServerDataBaseConnection.Interface
{
    public interface IDataBaseConnection
    {
        string DatabaseName { get; set; }
        string DataSource { get; set; }
        string Password { get; set; }
        string UserId { get; set; }
    }
}