using ApiFatecWeb.Code.Helpers;
using ApiFatecWeb.Core.Model;
using ApiFatecWeb.Core.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiFatecWeb.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/ticketMessages")]
    public class TicketMessagesController : BaseController
    {
        private readonly ITicketMessagesService _ticketMessagesService;
        private readonly ILogHandler _log;

        public TicketMessagesController(ITicketMessagesService ticketMessagesService, ILogHandler log)
        {
            _ticketMessagesService = ticketMessagesService;
            _log = log;
        }

        [HttpGet("{idTicket}")]
        public async Task<IActionResult> ListByTicketId(int idTicket)
        {
            try
            {
                return Ok(await _ticketMessagesService.List(idTicket, CurrentUser, CurrentUserRole));
            }
            catch (Exception ex)
            {
                _log.SaveLog("API | ERROR | TicketMessagesController/List", ex.Message, CurrentUser);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Insert(TicketMessagesInsertModel model)
        {
            try
            {
                return Ok(await _ticketMessagesService.Insert(model, CurrentUser, CurrentUserRole));
            }
            catch (Exception ex)
            {
                _log.SaveLog("API | ERROR | TicketMessagesController/Insert", ex.Message, CurrentUser);
                return BadRequest(ex.Message);
            }
        }



    }
}
