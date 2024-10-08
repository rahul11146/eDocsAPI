using Dapper;
using eDocsAPI.Data;
using eDocsAPI.Interface;
using eDocsAPI.Models;

namespace eDocsAPI.Repository
{
    public class ProjectRepository : IProject
    {
        private readonly SQLDbContext context;

        public ProjectRepository(SQLDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Project>> Get()
        {
            var sql = $@"SELECT [ProductId],
                               [ProductName],
                               [Price],
                               [ProdcutDescription],
                               [CreatedOn],
                               [UpdateOn]
                            FROM 
                               [Products]";

            using var connection = context.CreateConnection();
            return await connection.QueryAsync<Project>(sql);
        }
        public async Task<Project> Find(string id)
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
                              [ProductId]=@uid";

            using var connection = context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Project>(sql, new { id });
        }
        public async Task Add(Project model)
        {
            model.ProjectId = 0;
            using var connection = context.CreateConnection();
            await connection.ExecuteAsync(StoredProcedures.SP_Project_UPSERT, new { model.ProjectId, model.ProjectName, model.IsActive, model.CreatedBy, model.UpdatedBy });
        }
        public async Task Update(Project model)
        {
            using var connection = context.CreateConnection();
            await connection.ExecuteAsync(StoredProcedures.SP_Project_UPSERT, new { model.ProjectId, model.ProjectName, model.IsActive, model.CreatedBy, model.UpdatedBy });
        }
        public async Task<Project> Remove(Project model)
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
