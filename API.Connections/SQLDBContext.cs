using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace API.Connections
{
    public class SQLDBContext : DbContext
    {
        public SQLDBContext(DbContextOptions<SQLDBContext> options) : base(options) { }

        public DbSet<MyEntity> MyEntities { get; set; }
    }
}

public class MyEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}
