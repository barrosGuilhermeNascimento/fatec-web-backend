using ApiFatecWeb.Core.Model.Enum;
using Microsoft.AspNetCore.Mvc;

namespace ApiFatecWeb.Controllers
{
    [ApiController]

    public class BaseController : Controller
    {
        private int _currentUser { get; set; }
        protected int CurrentUser
        {
            get
            {
                if (_currentUser == 0)
                {
                    _currentUser = int.Parse(HttpContext.User.Claims.First(x => x.Type == "userId").Value);
                }

                return _currentUser;
            }
        }

        private RoleEnum _currentUserRole { get; set; }

        protected RoleEnum CurrentUserRole
        {
            get
            {
                if (_currentUserRole == null)
                {
                    _currentUserRole = (RoleEnum) Enum.Parse(typeof(RoleEnum),
                        HttpContext.User.Claims.First(x => x.Type == "roleId").Value);
                }

                return _currentUserRole;
            }
        }

    }
}
