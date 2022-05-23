using ApiFatecWeb.Core.Database;
using ApiFatecWeb.Core.Entity;
using ApiFatecWeb.Core.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiFatecWeb.Core.Repository
{
    public class TicketMessagesRepository: ITicketMessagesRepository
    {
        private readonly BaseDbContext _context;

        public TicketMessagesRepository(BaseDbContext context)
        {
            _context = context;
        }

        public TicketMessagesEntity? GetByTicketMessageId(int ticketMessageId)
        {
            return _context.TicketMessages.FirstOrDefault(ticket => ticket.IdTicketMessages== ticketMessageId);
        }

        public async Task<TicketMessagesEntity?> GetByTicketMessageIdAsync(int ticketMessageId)
        {
            return await _context.TicketMessages.FirstOrDefaultAsync(ticket => ticket.IdTicketMessages == ticketMessageId);
        }


        public IEnumerable<TicketMessagesEntity?> ListByTicketId(int ticketId)
        {
            return _context.TicketMessages.Where(ticket => ticket.IdTicket == ticketId);
        }

        public async Task<IEnumerable<TicketMessagesEntity?>> ListByTicketIdAsync(int ticketId)
        {
            return await _context.TicketMessages.Where(ticket => ticket.IdTicket == ticketId).ToListAsync();
        }

        public async Task<TicketMessagesEntity> Insert(TicketMessagesEntity entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
