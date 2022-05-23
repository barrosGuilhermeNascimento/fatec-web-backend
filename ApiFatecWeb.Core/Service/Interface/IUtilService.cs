using ApiFatecWeb.Core.Entity;

namespace ApiFatecWeb.Core.Service.Interface
{
    public interface IUtilService
    {
        Task<List<RoleEntity>> ListRoles();
    }
}
