using AutoMapper;
using AutoMapper.QueryableExtensions;
using FileSharingApp.Areas.Admin.Models;
using FileSharingApp.RepositoryPattern.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace FileSharingApp.Areas.Admin.Controllers
{
    public class ContactUsController : AdminBaseController
    {
        private readonly IUnitOfWork ufw;
        private readonly IMapper mapper;

        public ContactUsController(IUnitOfWork ufw,IMapper mapper)
        {
            this.ufw = ufw;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string searchInput,int pageNum = 1, int pageSize = 10)
        {
            var messages = ufw.ContactUs.GetAll(c=>searchInput != null?(c.User.FirstName+c.User.LastName).Contains(searchInput):true).Skip((pageNum-1)*pageSize).Take(pageSize).ProjectTo<ContactUsAdminViewModel>(mapper.ConfigurationProvider);
            ViewBag.IsLastPage = pageNum*pageSize >= await ufw.ContactUs.CountAsync();
            ViewBag.PageNum = pageNum;
            ViewBag.SearchInput = searchInput;
            return View(messages);

        }
        [HttpGet]
        public async Task<IActionResult> MarkAsRead(int id,int pageNum,string searchInput)
        {
            var message = await ufw.ContactUs.FindAsync(id);
            message.IsRead = true;
            await ufw.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new {pageNum,searchInput});
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var message = await ufw.ContactUs.FirstOrDefaultAsync(c=>c.Id==id,"User"); 
            return View(mapper.Map<ContactUsAdminViewModel>(message));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id,int pageNum,string searchInput)
        {
            var message = await ufw.ContactUs.FindAsync(id);
            if(message is not null)
            {
                ufw.ContactUs.Remove(message);
                await ufw.SaveChangesAsync();
                TempData["Message"] = "Succeeded";
            }
            else
                TempData["Message"] = "Failed";

            ViewBag.SerachInput = searchInput;

            return RedirectToAction(nameof(Index), new { pageNum,searchInput });

        }

        [HttpPost,HttpGet]
        public async Task<IActionResult> Search(string searchInput, int pageNum = 1, int pageSize = 10)
        {
            var messages = ufw.ContactUs.Where(c => (c.User.FirstName + c.User.LastName).Contains(searchInput),"User").Skip((pageNum-1)*pageSize).Take(pageSize);
            ViewBag.IsLastPage = pageNum * pageSize >= await ufw.ContactUs.CountAsync();
            ViewBag.PageNum = pageNum;
            ViewBag.SearchInput = searchInput;
            return View(nameof(Index), mapper.Map<IEnumerable<ContactUsAdminViewModel>>(messages));
        }

    }
}
