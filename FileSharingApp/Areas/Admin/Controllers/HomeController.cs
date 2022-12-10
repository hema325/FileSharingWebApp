using Microsoft.AspNetCore.Mvc;

namespace FileSharingApp.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
