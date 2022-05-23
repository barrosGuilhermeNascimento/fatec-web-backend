using ApiFatecWeb.Code.Helpers;
using ApiFatecWeb.Core.Entity;
using ApiFatecWeb.Core.Model;
using ApiFatecWeb.Core.Model.Enum;
using ApiFatecWeb.Core.Repository.Interface;
using ApiFatecWeb.Core.Service.Interface;
using AutoMapper;

namespace ApiFatecWeb.Core.Service
{
    public class TicketMessagesService : ITicketMessagesService
    {
        private readonly ILogHandler _log;
        private readonly IMapper _mapper;
        private readonly ITicketMessagesRepository _ticketMessagesRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;

        public TicketMessagesService(ILogHandler log, IMapper mapper, ITicketMessagesRepository ticketMessagesRepository, ITicketRepository ticketRepository, IUserRepository userRepository)
        {
            _log = log;
            _mapper = mapper;
            _ticketMessagesRepository = ticketMessagesRepository;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
        }

        public async Task<List<TicketMessagesModel>> List(int idTicket, int idUser, RoleEnum role)
        {
            try
            {
                var ticket = await _ticketRepository.GetByIdAsync(idTicket);
                if (ticket == null)
                {
                    throw new Exception("Ticket não encontrado");
                }

                if (role == RoleEnum.Client)
                {
                    if (ticket.IdCliente != idUser)
                    {
                        throw new Exception("Esse ticket não pertence a esse cliente");
                    }
                }

                var ticketMessages = (await _ticketMessagesRepository.ListByTicketIdAsync(idTicket)).ToList();

                var ticketMessagesModel = new List<TicketMessagesModel>();
                ticketMessages.ForEach(ticketMessagesEntity =>
                {
                    var model = _mapper.Map<TicketMessagesModel>(ticketMessagesEntity);
                    var user = _userRepository.GetOneById(model.IdUser);
                    model.NameUser = user?.Name;
                    model.UserRole = Enum.GetName(typeof(RoleEnum), user.IdRole);
                    ticketMessagesModel.Add(model);
                });

                return ticketMessagesModel;
            }
            catch (Exception ex)
            {
                _log.SaveLog("API | ERROR | TicketMessagesService/List", ex.Message, idUser);
                throw new Exception(ex.Message);
            }
        }

        public async Task<TicketMessagesModel> Insert(TicketMessagesInsertModel insertModel, int idUser, RoleEnum role)
        {
            try
            {
                var ticket = await _ticketRepository.GetByIdAsync(insertModel.IdTicket);
                if (ticket == null)
                {
                    throw new Exception("Ticket não encontrado");
                }

                if (insertModel.Message.Length < 2)
                {
                    throw new Exception("Mensagem com no mínimo 2 caracteres obrigatório");
                }

                if (role == RoleEnum.Client)
                {
                    if (ticket.IdCliente != idUser)
                    {
                        throw new Exception("Esse ticket não pertence a esse cliente");
                    }
                }

                var entity = _mapper.Map<TicketMessagesEntity>(insertModel);
                entity.IdUser = idUser;
                entity.DtSended = DateTime.Now;

                var model = _mapper.Map<TicketMessagesModel>(await _ticketMessagesRepository.Insert(entity));
                return model;
            }
            catch (Exception ex)
            {
                _log.SaveLog("API | ERROR | TicketMessagesService/Insert", ex.Message, idUser);
                throw new Exception(ex.Message);
            }
        }
    }
}
