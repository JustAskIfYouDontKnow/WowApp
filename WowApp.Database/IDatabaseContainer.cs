using WowApp.Database.User;

namespace WowApp.Database;

public interface IDatabaseContainer
{
    IUserRepository User { get; set; }
}