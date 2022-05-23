using ApiFatecWeb.Core.Entity;

namespace ApiFatecWeb.Core.Repository.Interface
{
    public interface IRolesRepository
    {
        RoleEntity? GetRoleById(int id);
        Task<RoleEntity?> GetRoleByIdAsync(int id);
        List<RoleEntity> ListRoles();
        Task<List<RoleEntity>> ListRolesAsync();
    }
}
