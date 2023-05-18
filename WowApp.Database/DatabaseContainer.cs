using WowApp.Database.User;

namespace WowApp.Database;

public class DatabaseContainer : IDatabaseContainer
{
    public IUserRepository User { get; set; }
    
    public DatabaseContainer(PostgresContext db)
    {
        User = new UserRepository(db);
    }
}