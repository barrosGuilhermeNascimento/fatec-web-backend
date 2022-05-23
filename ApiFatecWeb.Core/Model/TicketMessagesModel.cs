using System.ComponentModel.DataAnnotations;

namespace ApiFatecWeb.Core.Model
{
    public class TicketMessagesModel
    {
        public int? IdTicketMessages { get; set; }

        [Required]
        public int IdTicket { get; set; }

        [Required]
        public int IdUser { get; set; }

        public string? NameUser { get; set; }

        public string? UserRole { get; set; }

        [Required, MinLength(2)]
        public string Message { get; set; }
        
        public DateTime? DtSended { get; set; }

    }

    public class TicketMessagesInsertModel
    {

        [Required]
        public int IdTicket { get; set; }
        
        [Required, MinLength(2)]
        public string Message { get; set; }
    }
}
