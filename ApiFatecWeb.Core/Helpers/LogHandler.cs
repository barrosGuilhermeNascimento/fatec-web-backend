using ApiFatecWeb.Core.Entity;
using ApiFatecWeb.Core.Repository.Interface;

namespace ApiFatecWeb.Code.Helpers
{
    public interface ILogHandler
    {
        void SaveLog(string location, string message, int idUser);
    }
    public class LogHandler : ILogHandler
    {
        private readonly ILogRepository _logRep;
        public LogHandler(ILogRepository logRep)
        {
            _logRep = logRep;
        }

        public void SaveLog(string location, string message, int idUser)
        {
            var entity = new LogEntity();
            entity.NmTable = $"API | {location}";
            entity.Description = message;
            entity.IdUser = idUser;
            entity.DtUpdate = DateTime.Now;

            _logRep.Insert(entity);
        }

    }
}
