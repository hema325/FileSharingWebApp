using FileSharingApp.Constraints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileSharingApp.Areas.Admin.Controllers
{
    [Area(AreaNames.Admin)]
    [Authorize(Roles = IdentityRoles.Admin)]
    public abstract class AdminBaseController : Controller
    {

    }
}
