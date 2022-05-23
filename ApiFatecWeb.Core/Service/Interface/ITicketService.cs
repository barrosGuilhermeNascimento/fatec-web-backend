using ApiFatecWeb.Core.Entity;
using ApiFatecWeb.Core.Model;

namespace ApiFatecWeb.Core.Service.Interface
{
    public interface ITicketService
    {
        Task<TicketModel> GetTicketById(int id);
        Task<List<TicketModel>> List(int userId = 0);
        Task<List<TicketStatusEntity>> ListStatus();
        Task<TicketModel> Insert(TicketInsertModel ticket);
        Task<TicketModel> Update(int id, TicketUpdateModel ticket);
    }
}
