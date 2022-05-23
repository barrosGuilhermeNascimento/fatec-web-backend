using System.ComponentModel.DataAnnotations.Schema;
using ApiFatecWeb.Core.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace ApiFatecWeb.Core.Database
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
            
        }

        public DbSet<LogEntity> Log { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<TicketEntity> Ticket { get; set; }
        public DbSet<TicketMessagesEntity> TicketMessages { get; set; }
        public DbSet<TicketStatusEntity> TicketStatus { get; set; }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<UserPassRecover> UserPassRecover { get; set; }

    }
}
