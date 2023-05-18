using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using WowApp.Database.User.Path;

namespace WowApp.Database.User;

public class UserRepository : AbstractRepository<UserModel>, IUserRepository
{
    public UserRepository(PostgresContext context) : base(context) { }
    
    public async Task<UserModel> Create(string firstName, string lastName, string email)
    {
        var model = UserModel.CreateModel(firstName, lastName, email);

        var result = await CreateModelAsync(model);

        if (result is null)
        {
            throw new RpcException(new Status(StatusCode.Aborted, "User model is not created."));
        }

        return result;
    }
    
    public async Task<UserModel> GetOneById(int id)
    {
        var user = await DbModel.FindAsync(id);

        if (user is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"User ID: {id} is not found"));
        }

        return user;
    }
    
    public async Task<List<UserModel>> GetAllUsers()
    {
        return await DbModel.OrderBy(x=> x.Id).ToListAsync();
    }
    
    public async Task<UserModel> Update(UserModel userModel, UserPath userPath)
    {
        if (userModel.IsSame(userPath))
        {
            return userModel;
        }
        
        userModel.UpdateByUserPatch(userPath);
        
        var result = await UpdateModelAsync(userModel);
        
        if (result == 0)
        {
            throw new RpcException(new Status(StatusCode.Aborted,$"User ID: {userModel.Id} is not updated"));
        }
        return userModel;
    }
    
    public async Task<bool> Delete(UserModel user)
    {
        await DeleteModel(user);
        return true;
    }
}