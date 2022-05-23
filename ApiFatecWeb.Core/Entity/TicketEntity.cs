using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFatecWeb.Core.Entity;

[Table("ticket")]
public class TicketEntity
{
    [Key]
    public int IdTicket { get; set; }
    public Guid Identifier { get; set; }
    public int IdCliente { get; set; }
    public int? IdOperator { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public int StatusId { get; set; }
    public DateTime DtOpen { get; set; }
    public DateTime DtUpdated { get; set; }

    //[ForeignKey("idCliente")]
    //public virtual ICollection<UserEntity> Client { get; set; }

    //[ForeignKey("idOperator")]
    //public virtual ICollection<UserEntity> Operator { get; set; }
}