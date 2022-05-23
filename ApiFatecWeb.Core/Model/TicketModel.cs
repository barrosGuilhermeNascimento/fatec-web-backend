namespace ApiFatecWeb.Core.Model
{
    public class TicketModel
    {
        public int IdTicket { get; set; }
        public Guid Identifier { get; set; }
        public int IdCliente { get; set; }
        public string NameCliente { get; set; }
        public int IdOperator { get; set; }
        public string NameOperator { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int StatusId { get; set; }
        public string? Status { get; set; }
        public DateTime DtOpen { get; set; }
        public DateTime DtUpdated { get; set; }
    }


    public class TicketInsertModel
    {
        public int IdCliente { get; set; }
        public int IdOperator { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int StatusId { get; set; }
    }

    public class TicketUpdateModel
    {
        public int IdOperator { get; set; }
        public int StatusId { get; set; }
    }
}
