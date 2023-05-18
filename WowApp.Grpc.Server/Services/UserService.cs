using Grpc.Core;
using WowApp.Database;
using WowApp.Database.User.Path;
using WowApp.Grpc.Main;

namespace WowApp.Grpc.Services
{
    public class UserService : Main.UserService.UserServiceBase
    {
        private readonly IDatabaseContainer _databaseContainer;
    
        public UserService(IDatabaseContainer databaseContainer)
        {
            _databaseContainer = databaseContainer;
        }

        public async override Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            var userModel = await _databaseContainer.User.GetOneById(request.UserId);
            
            return new GetUserResponse()
            {
                User = new User()
                {
                    UserId = userModel.Id,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    Email = userModel.Email
                }
            };
        }
        
        public async override Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
          
            var userModel = await _databaseContainer.User.Create(request.User.FirstName, request.User.LastName, request.User.Email);
           
            return new CreateUserResponse()
            {
                User = new User()
                {
                    UserId = userModel.Id,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    Email = userModel.Email
                }
            };
        }
        
        public async override Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
        {
            var userModel = await _databaseContainer.User.GetOneById(request.User.UserId);
            
            var userPath = new UserPath(request.User.FirstName,request.User.LastName, request.User.Email);
            
            var updatedUser = await _databaseContainer.User.Update(userModel, userPath);

            return new UpdateUserResponse
            { 
                User = new User() 
                {
                    UserId = updatedUser.Id, 
                    FirstName = updatedUser.FirstName, 
                    LastName = updatedUser.LastName, 
                    Email = updatedUser.Email
                } 
            };
        }
        
        public async override Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
        {
            var userModel = await _databaseContainer.User.GetOneById(request.UserId);
            var success = await _databaseContainer.User.Delete(userModel);
            
            return new DeleteUserResponse
            {
                Success = success
            };
        }


        public async override Task<GetAllUsersResponse> GetAllUsers(GetAllUsersRequest request, ServerCallContext context)
        {
            var userModels = await _databaseContainer.User.GetAllUsers();

            return new GetAllUsersResponse
            {
                Users =
                {
                    userModels.Select(
                            userModel => new User
                            {
                                UserId = userModel.Id,
                                FirstName = userModel.FirstName,
                                LastName = userModel.LastName,
                                Email = userModel.Email
                            }
                        ).ToList()
                }
            };
        }
    }
}
