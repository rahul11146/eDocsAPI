using Dapper;
using eDocsAPI.Data;
using eDocsAPI.Interface;
using eDocsAPI.Models;
using System.Reflection;

namespace eDocsAPI.Repository
{
    public class UserRepository : IUser
    {
        private readonly SQLDbContext context;

        public UserRepository(SQLDbContext context)
        {
            this.context = context;
        }

        public async Task<IList<Users>> Get(string IsActive)
        {
            using var connection = context.CreateConnection();
            var result = await connection.QueryAsync<Users>(StoredProcedures.SP_Project_GetList, new { IsActive });

            return result.ToList();
        }
        public async Task<Users?> Find(string id)
        {
            var sql = $@"SELECT [ProductId],
                               [ProductName],
                               [Price],
                               [ProdcutDescription],
                               [CreatedOn],
                               [UpdateOn]
                            FROM 
                               [Products]
                            WHERE
                              [ProductId]=@uid
            ";

            using var connection = context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Users>(sql, new { id });
        }

        public async Task<Users?> Authenticate(string username, string password)
        {
            using var connection = context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Users>(StoredProcedures.SP_User_GetById, new { username = username, password = password });
        }
        public async Task Add(Users model)
        {
            model.UserId = 0;
            using var connection = context.CreateConnection();
            await connection.ExecuteAsync(StoredProcedures.SP_Project_UPSERT, new { model.UserId, model.UserName, model.IsActive, model.CreatedBy, model.UpdatedBy });
        }
        public async Task Update(Users model)
        {
            using var connection = context.CreateConnection();
            await connection.ExecuteAsync(StoredProcedures.SP_Project_UPSERT, new { model.UserId, model.UserName, model.IsActive, model.CreatedBy, model.UpdatedBy });
        }
        public async Task<Users> Remove(Users model)
        {
            var sql = $@"
                        DELETE FROM
                            [dbo].[Products]
                        WHERE
                            [ProductId]=@ProductId";
            using var connection = context.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }

    }
}
