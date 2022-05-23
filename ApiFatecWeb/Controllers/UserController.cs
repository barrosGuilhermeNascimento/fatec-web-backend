using ApiFatecWeb.Code.Helpers;
using ApiFatecWeb.Configuration;
using ApiFatecWeb.Core.Model;
using ApiFatecWeb.Core.Model.ErrorResponse;
using ApiFatecWeb.Core.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiFatecWeb.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogHandler _log;
        private readonly IMapper _map;
        public UserController(IUserService userService, ILogHandler log, IMapper map)
        {
            _userService = userService;
            _log = log;
            _map = map;
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DefaultErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(UserLoginModel login)
        {
            var user = await _userService.Login(login);

            if (user == null)
            {
                return BadRequest();
            }

            var returnUser = _map.Map<UserModelLogin>(user);
            returnUser.token = getToken(HttpContext, user);

            _log.SaveLog("login", $"User {user.Name} logged in", user.IdUser);
            return Ok(returnUser);
        }


        [AllowAnonymous]
        [HttpPost("")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DefaultErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(UserRegisterModel user)
        {
            try
            {
                return Ok(await _userService.Register(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Authorize]
        [HttpGet("{email}")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DefaultErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(DefaultErrorResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetOneUser(string email)
        {
            var entity = await _userService.GetOneByEmailAsync(email);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Password = "";
            return Ok(entity);
        }

        [Authorize]
        [HttpGet("list/{idRole?}")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DefaultErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(DefaultErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListUsers(int? idRole)
        {
            var entity = await _userService.ListAsync(idRole ?? 0);
            return Ok(entity);
        }
        
        [HttpGet("recoverPassword/{email?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DefaultErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RecoverPassword(string email)
        {
            try
            {
                _log.SaveLog("API | User | recoverPassword", "Getting user", 0);
                var user = await _userService.GetOneByEmailAsync(email);
                if (user == null)
                {
                    return NotFound();
                }
                _log.SaveLog("API | User | recoverPassword", "User found", user.IdUser);

                var returnService = await _userService.RecoverPassword(user);
                _log.SaveLog("API | User | recoverPassword", "Return RecoverPass: "+ returnService , user.IdUser);

                return returnService ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                _log.SaveLog("API | User | recoverPassword", ex.Message, 0);
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("changePassword")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DefaultErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(UserChangePasswordModel userRecover)
        {
            try
            {
                await _userService.ChangePassword(userRecover);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new DefaultErrorResponse()
                {
                    Message = ex.Message
                });
            }
        }




        private string getToken(HttpContext context, UserModel user)
        {
            var token = TokenMiddleware.GenerateToken(user);
            return token;
        }
    }
}
