using ApiFatecWeb.Core.Entity;
using ApiFatecWeb.Core.Model;

namespace ApiFatecWeb.Core.Service.Interface
{
    public interface IUserService
    {
        Task<UserModel> GetOneByEmailAsync(string email);
        Task<List<UserModel>> ListAsync(int idRole = 0);
        Task<UserModel?> Login(UserLoginModel login);
        Task<UserModel> Register(UserRegisterModel register);
        Task<bool> RecoverPassword(UserModel user);
        Task ChangePassword(UserChangePasswordModel model);
    }
}
