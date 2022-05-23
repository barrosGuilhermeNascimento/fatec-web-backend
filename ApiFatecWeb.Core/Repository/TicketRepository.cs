using ApiFatecWeb.Core.Database;
using ApiFatecWeb.Core.Entity;
using ApiFatecWeb.Core.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiFatecWeb.Core.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly BaseDbContext _context;
        public TicketRepository(BaseDbContext context)
        {
            _context = context;
        }

        public TicketEntity? GetById(int id)
        {
            return _context.Ticket.FirstOrDefault(ticket => ticket.IdTicket == id);
        }

        public async Task<TicketEntity?> GetByIdAsync(int id)
        {
            return await _context.Ticket.FirstOrDefaultAsync(ticket => ticket.IdTicket == id);
        }

        public TicketEntity? GetByUserId(int userId)
        {
            return _context.Ticket.FirstOrDefault(ticket => ticket.IdCliente == userId || ticket.IdOperator == userId);
        }

        public async Task<TicketEntity?> GetByUserIdAsync(int userId)
        {
            return await _context.Ticket.FirstOrDefaultAsync(ticket => ticket.IdCliente == userId || ticket.IdOperator == userId);
        }

        public string? GetStatusNameById(int statusId)
        {
            return (_context.TicketStatus.FirstOrDefault(status => status.IdStatus == statusId))?.Name;
        }

        public List<TicketEntity> List()
        {
            return _context.Ticket.ToList();
        }

        public async Task<List<TicketEntity>> ListAsync()
        {
            return await _context.Ticket.ToListAsync();
        }

        public async Task<List<TicketStatusEntity>> ListStatus()
        {
            return await _context.TicketStatus.ToListAsync();
        }

        public async Task<TicketEntity> Insert(TicketEntity entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TicketEntity> Update(TicketEntity entity)
        {
            var inDb = await _context.Ticket.FirstOrDefaultAsync(item => item.IdTicket == entity.IdTicket);
            if (inDb == null) throw new Exception("entity wasnt found");
                
            inDb.IdOperator = entity.IdOperator;
            inDb.DtUpdated = DateTime.Now;
            inDb.StatusId = entity.StatusId;
            await _context.SaveChangesAsync();
            return entity;
        }


    }
}
