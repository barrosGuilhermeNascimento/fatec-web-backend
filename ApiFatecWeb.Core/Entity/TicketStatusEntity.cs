using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFatecWeb.Core.Entity
{
    [Table("ticket_status")]
    public class TicketStatusEntity
    {
        [Key]
        public int IdStatus { get; set; }

        public string Name { get; set; }
    }
}
