using ApiFatecWeb.Code.Helpers;
using ApiFatecWeb.Core.Database;
using ApiFatecWeb.Core.Entity;
using ApiFatecWeb.Core.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiFatecWeb.Core.Repository
{
    public class UserPassRecoverRepository : IUserPassRecoverRepository
    {
        private readonly ILogHandler _logHandler;
        private readonly BaseDbContext _context;

        public UserPassRecoverRepository(ILogHandler logHandler, BaseDbContext context)
        {
            _logHandler = logHandler;
            _context = context;
        }

        public async Task SaveRecover(int idUser, int number)
        {
            var entity = new UserPassRecover()
            {
                IdUser = idUser,
                RecoverNumber = number,
                DateCreated = DateTime.Now 
            };
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CodeExists (int idUser)
        {
            var difDate = DateTime.Now.AddMinutes(-15);
            var entity =
                await _context.UserPassRecover.FirstOrDefaultAsync(item =>
                    item.IdUser == idUser && item.DateCreated > difDate);
            return entity.RecoverNumber;
        }
    }
}
