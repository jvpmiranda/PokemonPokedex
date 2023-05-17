using SqlServerDataBaseConnection.Interface;

namespace SqlServerDataBaseConnection.Model
{
    public class DataBaseConnection : IDataBaseConnection
    {
        public string DataSource { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
    }
}
