using AutoMapper;
using AutoMapper.QueryableExtensions;
using FileSharingApp.Areas.Identity.Controllers;
using FileSharingApp.Constraints;
using FileSharingApp.Hubs;
using FileSharingApp.Models;
using FileSharingApp.RepositoryPattern.IRepository;
using FileSharingApp.Services.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;

namespace FileSharingApp.Areas.Users.Controllers
{
    public class HomeController : UsersBaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUnitOfWork ufw;
        private readonly IMapper mapper;
        private readonly IHubContext<Notifications> hubContext;
        private readonly UserManager<ApplicationUser> userManager;
        public HomeController(ILogger<AccountController> logger, IUnitOfWork ufw, IMapper mapper,IHubContext<Notifications> hubContext,UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            this.ufw = ufw;
            this.mapper = mapper;
            this.hubContext = hubContext;
            this.userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.PopularFiles = ufw.uploadRepository.GetAll().OrderByDescending(file => file.NumberOfDownLoads).Take(8).OrderByDescending(file => file.NumberOfDownLoads).ProjectTo<ListUploadViewModel>(mapper.ConfigurationProvider);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Contact([FromServices] IEmailSender emailSender, ContactViewModel model)
        {
            if (!ModelState.IsValid)
                return NotFound();

            var contact = mapper.Map<Contact>(model);
            contact.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await ufw.contactRepository.AddAsync(contact);
            await ufw.SaveChangesAsync();

            TempData["Succee"] = "Message Sent Succefully";

            await emailSender.SendEmailAsync(contact);

            var admins = await userManager.GetUsersInRoleAsync(IdentityRoles.Admin);

            if (admins is not null)
            {
                await hubContext.Clients.Users(admins.Select(u=>u.Id)).SendAsync("RecieveNotifications", DateTime.Now);
            }

            return RedirectToAction(nameof(Contact));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Culture(string culture, string returnUrl)
        {

            if (!string.IsNullOrEmpty(culture))
            {
                Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddYears(1)
                    });
            }

            if (!string.IsNullOrEmpty(returnUrl))
                return LocalRedirect(returnUrl);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> notify()
        {
            var admins = await userManager.GetUsersInRoleAsync(IdentityRoles.Admin);

            if (admins is not null)
            {
                await hubContext.Clients.Users(admins.Select(u => u.Id)).SendAsync("RecieveNotifications", DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss",new CultureInfo("en-US")));
            }

            return LocalRedirect(Request.Path);
        }

    }
}