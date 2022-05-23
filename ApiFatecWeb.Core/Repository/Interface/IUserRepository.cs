using ApiFatecWeb.Core.Entity;

namespace ApiFatecWeb.Core.Repository.Interface
{
    public interface IUserRepository
    {
        UserEntity? GetOneByEmail(string email);
        Task<UserEntity?> GetOneByEmailAsync(string email);

        UserEntity? GetOneById(int id);
        Task<UserEntity?> GetOneByIdAsync(int id);

        IEnumerable<UserEntity?> List();
        Task<IEnumerable<UserEntity?>> ListAsync();
        Task<bool> Save(UserEntity user);
        Task<bool> Update(UserEntity user);

    }
}
