using ApiFatecWeb.Core.Entity;
using ApiFatecWeb.Core.Model;
using ApiFatecWeb.Core.Repository.Interface;
using ApiFatecWeb.Core.Service.Interface;
using AutoMapper;

namespace ApiFatecWeb.Core.Service
{
    public class TicketService : ITicketService
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;

        public TicketService(IMapper mapper, ITicketRepository ticketRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
        }

        public async Task<TicketModel> GetTicketById(int id)
        {
            var entity = await _ticketRepository.GetByIdAsync(id);
            var model = _mapper.Map<TicketModel>(entity);

            model.NameCliente = (await _userRepository.GetOneByIdAsync(model.IdCliente))?.Name ?? string.Empty;
            model.NameOperator = (await _userRepository.GetOneByIdAsync(model.IdOperator))?.Name ?? string.Empty;

            return model;
        }

        public async Task<List<TicketModel>> List(int userId = 0)
        {
            var tickets = await _ticketRepository.ListAsync();
            var listTicketModel = new List<TicketModel>();

            tickets.ForEach(ticket =>
            {
                var model = _mapper.Map<TicketModel>(ticket);
                model.NameCliente = (_userRepository.GetOneById(model.IdCliente))?.Name ?? string.Empty;
                model.NameOperator = (_userRepository.GetOneById(model.IdOperator))?.Name ?? string.Empty;
                model.Status = (_ticketRepository.GetStatusNameById(model.StatusId));
                if (userId == 0)
                {
                    listTicketModel.Add(model);
                }
                else if (ticket.IdCliente == userId || ticket.IdOperator == userId)
                {
                    listTicketModel.Add(model);
                }

            });

            return listTicketModel;
        }


        public async Task<List<TicketStatusEntity>> ListStatus()
        {
            return await _ticketRepository.ListStatus();
        }

        public async Task<TicketModel> Insert(TicketInsertModel ticket)
        {
            var model = _mapper.Map<TicketModel>(ticket);
            CheckTicket(model);

            model.DtOpen = DateTime.Now;
            model.DtUpdated = DateTime.Now;
            model.Identifier = Guid.NewGuid();

            var entity = _mapper.Map<TicketEntity>(model);
            var inserted = await _ticketRepository.Insert(entity);

            return _mapper.Map<TicketModel>(inserted);
        }

        public async Task<TicketModel> Update(int id, TicketUpdateModel ticket)
        {
            var model = await GetTicketById(id);
            model.IdOperator = ticket.IdOperator;
            model.StatusId = ticket.StatusId;
            model.DtUpdated = DateTime.Now;

            var entity = _mapper.Map<TicketEntity>(model);
            await _ticketRepository.Update(entity);

            return model;
        }

        private void CheckTicket(TicketModel ticket)
        {
            if (ticket.IdCliente == 0)
            {
                throw new Exception("Cliente é obrigatório!");
            }

            if (ticket.Title.Length == 0)
            {
                throw new Exception("Titulo é obrigatório");
            }
        }
    }
}
