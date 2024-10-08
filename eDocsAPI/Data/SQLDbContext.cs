using Microsoft.Data.SqlClient;
using System.Data;

namespace eDocsAPI.Data
{
    public class SQLDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connection;

        public SQLDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = configuration.GetConnectionString("DBConn");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connection);

    }
}
