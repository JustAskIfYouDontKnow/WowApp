using Microsoft.EntityFrameworkCore;
using WowApp.Database.User;

namespace WowApp.Database;

public class PostgresContext : DbContext
{
    public IDatabaseContainer Db { get; set; }
    
    public DbSet<UserModel> User { get; set; }


    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}