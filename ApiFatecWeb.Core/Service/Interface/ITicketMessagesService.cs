using ApiFatecWeb.Core.Model;
using ApiFatecWeb.Core.Model.Enum;

namespace ApiFatecWeb.Core.Service.Interface
{
    public interface ITicketMessagesService
    {
        Task<List<TicketMessagesModel>> List(int idTicket, int idUser, RoleEnum role);
        Task<TicketMessagesModel> Insert(TicketMessagesInsertModel model, int idUser, RoleEnum role);
    }
}
