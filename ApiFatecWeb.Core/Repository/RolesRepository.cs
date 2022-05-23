using ApiFatecWeb.Core.Database;
using ApiFatecWeb.Core.Entity;
using ApiFatecWeb.Core.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiFatecWeb.Core.Repository
{
    public class RolesRepository : IRolesRepository
    {
        private readonly BaseDbContext _context;

        public RolesRepository(BaseDbContext context)
        {
            _context = context;
        }

        public RoleEntity? GetRoleById(int id)
        {
            return _context.Roles.FirstOrDefault(role => role.IdRole == id);
        }

        public async Task<RoleEntity?> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(role => role.IdRole == id);
        }

        public List<RoleEntity> ListRoles()
        {
            return _context.Roles.ToList();
        }

        public async Task<List<RoleEntity>> ListRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
