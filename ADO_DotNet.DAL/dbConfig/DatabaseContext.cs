using Microsoft.Extensions.Configuration;

namespace ADO_DotNet.DAL.dbConfig
{
    public class DatabaseContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DbConnection");
        }

        public string ConnectionString { get { return _connectionString; } }
    }
}
