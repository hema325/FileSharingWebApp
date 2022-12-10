using AutoMapper;
using AutoMapper.QueryableExtensions;
using FileSharingApp.Areas.Admin.AdminRepositoryPattern.IRepository;
using FileSharingApp.Areas.Admin.Models;
using FileSharingApp.Constraints;
using Microsoft.AspNetCore.Mvc;

namespace FileSharingApp.Areas.Admin.Controllers
{
    public class ManagingUsersController : AdminBaseController
    {
        private readonly IAdminUnitOfWork ufw;
        private readonly IMapper mapper;
        public ManagingUsersController(IAdminUnitOfWork ufw,IMapper mapper)
        {
            this.ufw = ufw;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int pageNum = 1, int pageSize = 10)
        {
            var users = ufw.ManagingUsers.GetAll("User").Skip((pageNum-1)*pageSize).Take(pageSize).ProjectTo<UserAdminViewModel>(mapper.ConfigurationProvider);
            ViewBag.PageNum = pageNum;
            ViewBag.IsLastPage = pageSize * pageNum >= await ufw.ManagingUsers.CountAsync();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> BlockedUsers(int pageNum = 1, int pageSize = 10)
        {
            var BlockedUsers = ufw.ManagingUsers.GetAll(user => user.IsBlocked==true).Skip((pageNum - 1) * pageSize).Take(pageSize).ProjectTo<UserAdminViewModel>(mapper.ConfigurationProvider);
            ViewBag.PageNum = pageNum;
            ViewBag.IsLastPage = pageNum * pageSize >= await ufw.ManagingUsers.CountAsync();
            return View(BlockedUsers);
        }

        [HttpPost,HttpGet]
        public async Task<IActionResult> Search(string searchInput, int pageNum = 1, int pageSize = 10)
        {
            var users = ufw.ManagingUsers.GetAll(user => user.FirstName.Contains(searchInput) || user.LastName.Contains(searchInput) || user.Email.Contains(searchInput)).Skip((pageNum-1)*pageSize).Take(pageSize).ProjectTo<UserAdminViewModel>(mapper.ConfigurationProvider);
            ViewBag.PageNum = pageNum;
            ViewBag.IsLastPage = pageNum * pageSize >= await ufw.ManagingUsers.CountAsync();
            ViewBag.SearchInput = searchInput;
            return View("Index",users);
        }

        [HttpGet]
        public async Task<IActionResult> BlockUser(string id,string returnUrl)
        {
            bool Succeeded = false;
            if (!string.IsNullOrEmpty(id))
            {
                if(await ufw.ManagingUsers.BlockUserByIdAsync(id))
                {
                    TempData["Message"] = "Succeeded";
                    Succeeded = true;
                }
            }
            if (!Succeeded)
            {
                TempData["Message"] = "Failed";
            }
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> UnBlockUser(string id,string returnUrl)
        {
            if (!string.IsNullOrEmpty(id)&& await ufw.ManagingUsers.UnBlockUserByIdAsync(id))
            {
                TempData["Message"] = "Succeeded";
            }
            else
            TempData["Message"] = "Failed";

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult AddANewUser()
        {
            return RedirectToAction("Register", "Account", new { area = AreaNames.Identity });
        }



    }
}
