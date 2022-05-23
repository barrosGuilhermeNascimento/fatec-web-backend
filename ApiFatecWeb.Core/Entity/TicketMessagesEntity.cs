using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFatecWeb.Core.Entity;

[Table("ticket_messages")]
public class TicketMessagesEntity
{
    [Key]
    public int IdTicketMessages { get; set; }
    public int IdTicket { get; set; }
    public int IdUser { get; set; }
    public string message { get; set; }
    public DateTime DtSended { get; set; }

    //public virtual ICollection<UserEntity> User { get; set; }
}