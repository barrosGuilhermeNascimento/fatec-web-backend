using ApiFatecWeb.Core.Entity;

namespace ApiFatecWeb.Core.Repository.Interface
{
    public interface ITicketRepository
    {
        TicketEntity? GetById(int id);
        Task<TicketEntity?> GetByIdAsync(int id);
        TicketEntity? GetByUserId(int userId);
        Task<TicketEntity?> GetByUserIdAsync(int userId);
        string? GetStatusNameById(int statusId);
        List<TicketEntity> List();
        Task<List<TicketEntity>> ListAsync();
        Task<List<TicketStatusEntity>> ListStatus();
        Task<TicketEntity> Insert(TicketEntity entity);
        Task<TicketEntity> Update(TicketEntity entity);
    }
}
