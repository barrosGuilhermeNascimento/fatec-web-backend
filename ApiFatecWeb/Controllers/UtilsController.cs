using System.Text.Json.Serialization;
using ApiFatecWeb.Core.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiFatecWeb.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class UtilsController : BaseController
    {
        private readonly ITicketService _ticketService;
        private readonly IUtilService _utilService;

        public UtilsController(ITicketService ticketService, IUtilService utilService)
        {
            _ticketService = ticketService;
            _utilService = utilService;
        }

        [HttpGet("isWorking")]
        public IActionResult WorkingApi()
        {
            return Ok(new
            {
                message = "Api is working",
                time = DateTime.Now
            });
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _utilService.ListRoles());
        }

        [HttpGet("ticketStatus")]
        public async Task<IActionResult> GetTicketStatus()
        {
            return Ok(await _ticketService.ListStatus());
        }

        [HttpGet("priorities")]
        public async Task<IActionResult> GetPriorities()
        {
            var priorities = new List<object>
            {
                new
                {
                    id = 0,
                    text = "baixa"
                },
                new
                {
                    id = 1,
                    text = "média"
                },
                new
                {
                    id = 2,
                    text = "Alta"
                },
                new
                {
                    id = 3,
                    text = "Imediata"
                }
            };
            return Ok(priorities);
        }
    }
}
