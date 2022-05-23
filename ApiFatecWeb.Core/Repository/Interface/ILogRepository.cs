using ApiFatecWeb.Core.Entity;

namespace ApiFatecWeb.Core.Repository.Interface
{
    public interface ILogRepository
    {
        List<LogEntity?> List(string nmTable = "", int idUser = 0);
        void Insert(LogEntity log);
    }
}
