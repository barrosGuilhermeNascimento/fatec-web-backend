using ApiFatecWeb.Core.Entity;
using ApiFatecWeb.Core.Model;
using ApiFatecWeb.Core.Repository.Interface;
using ApiFatecWeb.Core.Service.Interface;

namespace ApiFatecWeb.Core.Service
{
    public class UtilService : IUtilService
    {
        private readonly IRolesRepository _rolesRepository;
        public UtilService(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }


        public async Task<List<RoleEntity>> ListRoles()
        {
            var rolesEntityList = await _rolesRepository.ListRolesAsync();
            return rolesEntityList;
        }

    }
}
