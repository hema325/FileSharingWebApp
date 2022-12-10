using FileSharingApp.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileSharingApp.Areas.Users.Controllers
{
    [Area(AreaNames.Users)]
    [Authorize(Roles = $"{IdentityRoles.User},{IdentityRoles.Admin}")]
    public abstract class UsersBaseController : Controller
    {
    }
}
