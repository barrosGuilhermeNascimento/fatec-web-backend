using ApiFatecWeb.Code.Helpers;
using ApiFatecWeb.Core.Model;
using ApiFatecWeb.Core.Model.ErrorResponse;
using ApiFatecWeb.Core.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiFatecWeb.Controllers
{
    [ApiController]
    [Route("ticket")]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ILogHandler _log;

        public TicketController(ITicketService ticketService, ILogHandler log)
        {
            _ticketService = ticketService;
            _log = log;
        }

        [Authorize]
        [HttpGet("list/{userId?}")]
        [ProducesResponseType(typeof(List<TicketModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DefaultErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DefaultErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> List(int? userId)
        {
            try
            {
                var listTicket = await _ticketService.List(userId ?? 0);

                if (!listTicket.Any())
                {
                    return NotFound();
                }

                return Ok(listTicket);
            }
            catch (Exception ex)
            {
                var error = new DefaultErrorResponse
                {
                    Message = ex.Message
                };

                _log.SaveLog("API | Error | Ticket/List", ex.Message, int.Parse(HttpContext.User.Claims.First(x => x.Type == "userId").Value));
                return BadRequest(error);
            }
        }

        [Authorize]
        [HttpGet("{ticketId}")]
        [ProducesResponseType(typeof(TicketModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DefaultErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DefaultErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get(int ticketId)
        {
            try
            {
                var listTicket = await _ticketService.GetTicketById(ticketId);

                if (listTicket == null)
                {
                    return NotFound();
                }

                return Ok(listTicket);
            }
            catch (Exception ex)
            {
                var error = new DefaultErrorResponse
                {
                    Message = ex.Message
                };

                _log.SaveLog("API | Error | Ticket/Get", ex.Message, int.Parse(HttpContext.User.Claims.First(x => x.Type.Contains("userdata")).Value));
                return BadRequest(error);
            }
        }

        [Authorize]
        [HttpPost("")]
        [ProducesResponseType(typeof(TicketModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DefaultErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DefaultErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create(TicketInsertModel ticket)
        {
            try
            {
                return Ok(await _ticketService.Insert(ticket));
            }
            catch (Exception ex)
            {
                var error = new DefaultErrorResponse
                {
                    Message = ex.Message
                };

                _log.SaveLog("API | Error | Ticket/Insert", ex.Message, int.Parse(HttpContext.User.Claims.First(x => x.Type.Contains("userdata")).Value));
                return BadRequest(error);
            }
        }

        [Authorize]
        [HttpPatch("{idTicket}")]
        [ProducesResponseType(typeof(TicketModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DefaultErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DefaultErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(int idTicket, [FromBody] TicketUpdateModel ticket)
        {
            try
            {
                return Ok(await _ticketService.Update(idTicket, ticket));
            }
            catch (Exception ex)
            {
                var error = new DefaultErrorResponse
                {
                    Message = ex.Message
                };

                _log.SaveLog("API | Error | Ticket/Insert", ex.Message, int.Parse(HttpContext.User.Claims.First(x => x.Type.Contains("userdata")).Value));
                return BadRequest(error);
            }
        }
    }
}
