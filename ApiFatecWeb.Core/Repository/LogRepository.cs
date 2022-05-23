using ApiFatecWeb.Core.Database;
using ApiFatecWeb.Core.Entity;
using ApiFatecWeb.Core.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiFatecWeb.Core.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly BaseDbContext _context;

        public LogRepository(BaseDbContext context)
        {
            _context = context;
        }

        public List<LogEntity?> List(string nmTable = "", int idUser = 0)
        {
            var logsList = _context.Log.Select(
                logs =>
                    (nmTable.Length <= 0 || logs.NmTable == nmTable) && (idUser == 0 || logs.IdUser == idUser) ? logs : null
            );

            return logsList.ToList();
        }

        public void Insert(LogEntity log)
        {
            _context.Add(log);
            _context.SaveChanges();
        }
    }
}
