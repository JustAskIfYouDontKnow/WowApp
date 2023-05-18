using WowApp.Database.User.Path;

namespace WowApp.Database.User;

public interface IUserRepository
{
    Task<UserModel> Create(string firstName, string lastName, string email);

    Task<UserModel> GetOneById(int id);
    
    Task<List<UserModel>> GetAllUsers();

    Task<UserModel> Update(UserModel userModel, UserPath userPath);
    Task<bool> Delete(UserModel user);
}