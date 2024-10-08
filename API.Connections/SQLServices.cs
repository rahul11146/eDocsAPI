using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Connections
{
    public class SQLServices
    {
        private readonly string _connectionString;

        public SQLServices(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<MyEntity>> GetEntitiesAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<MyEntity>("SELECT * FROM MyEntities");
            }
        }
    }
}