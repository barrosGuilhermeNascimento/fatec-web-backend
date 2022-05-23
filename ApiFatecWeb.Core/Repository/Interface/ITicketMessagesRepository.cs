using ApiFatecWeb.Core.Entity;

namespace ApiFatecWeb.Core.Repository.Interface
{
    public interface ITicketMessagesRepository
    {
        TicketMessagesEntity? GetByTicketMessageId(int ticketMessageId);
        Task<TicketMessagesEntity?> GetByTicketMessageIdAsync(int ticketMessageId);
        IEnumerable<TicketMessagesEntity?> ListByTicketId(int ticketId);
        Task<IEnumerable<TicketMessagesEntity?>> ListByTicketIdAsync(int ticketId);
        Task<TicketMessagesEntity> Insert(TicketMessagesEntity entity);
    }
}
