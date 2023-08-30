using AutoMapper;
using AutoMapper.QueryableExtensions;
using FileSharingApp.Models;
using FileSharingApp.RepositoryPattern.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FileSharingApp.Areas.Users.Controllers
{
    [Authorize]
    [Area("Users")]
    public class UploadController : UsersBaseController
    {
        private readonly IUnitOfWork ufw;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper mapper;
        private string userId { get { return User.FindFirstValue(ClaimTypes.NameIdentifier); } }

        public UploadController(IUnitOfWork ufw, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            this.ufw = ufw;
            this.webHostEnvironment = webHostEnvironment;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var uploads = ufw.uploadRepository.Where(upload => upload.UserId == userId).OrderByDescending(file => file.NumberOfDownLoads).ProjectTo<ListUploadViewModel>(mapper.ConfigurationProvider);
            return View(uploads);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<IActionResult> Create(UploadViewModel model)
        {
            if (ModelState.IsValid && model.File is not null)
            {
                var root = webHostEnvironment.WebRootPath;
                var newFileName = string.Concat(Guid.NewGuid(), Path.GetExtension(model.File.FileName));
                var folder = "Files";
                var path = Path.Combine(root, folder, newFileName);

                using (var fileStream = System.IO.File.Create(path))
                {
                    await model.File.CopyToAsync(fileStream);
                }

                var upload = new Upload
                {
                    FileName = model.File.FileName,
                    Url = Path.Combine(folder, newFileName),
                    FileSize = model.File.Length,
                    ContentType = model.File.ContentType,
                    UploadedDateTime = DateTime.Now,
                    UserId = userId
                };

                await ufw.uploadRepository.AddAsync(upload);
                await ufw.SaveChangesAsync();

                return View(model);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return NotFound();

            var upload = await ufw.uploadRepository.FindAsync(new Guid(Id));

            if (upload.UserId != userId)
                return NotFound();

            var listUploadViewModel = mapper.Map<ListUploadViewModel>(upload);

            return View(listUploadViewModel);
        }

        [HttpPost]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteUploadedFile(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return NotFound();

            var upload = await ufw.uploadRepository.FindAsync(new Guid(Id));

            if (upload.UserId != userId)
                return NotFound();

            try
            {
                ufw.uploadRepository.Remove(upload);
                await ufw.SaveChangesAsync();
                var path = Path.Combine(webHostEnvironment.WebRootPath, upload.Url);
                System.IO.File.Delete(path);
            }
            catch
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> Search(string fileName, int pageNum = 1, int pageSize = 5)
        {
            var files = ufw.uploadRepository.Where(upload => !string.IsNullOrEmpty(fileName) ? upload.FileName.Contains(fileName) : true).Skip((pageNum - 1) * pageSize).Take(pageSize).OrderByDescending(file => file.UploadedDateTime).ProjectTo<ListUploadViewModel>(mapper.ConfigurationProvider);

            ViewBag.fileName = fileName;
            ViewBag.pageNum = pageNum;
            ViewBag.isLastPage = pageNum * pageSize >= await ufw.uploadRepository.Where(upload => upload.FileName.Contains(fileName)).CountAsync();

            return View(files);
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> Explore(int pageNum = 1, int pageSize = 5)
        {
            var files = ufw.uploadRepository.GetAll().OrderByDescending(file => file.NumberOfDownLoads).Skip((pageNum - 1) * pageSize).Take(pageSize).ProjectTo<ListUploadViewModel>(mapper.ConfigurationProvider);

            ViewBag.pageNum = pageNum;
            ViewBag.isLastPage = pageNum * pageSize >= await ufw.uploadRepository.CountAsync();

            return View(nameof(Search), files);
        }

        [HttpGet]

        public async Task<IActionResult> DownLoad(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return NotFound();

            var upload = await ufw.uploadRepository.FindAsync(new Guid(Id));
            if (upload is not null)
            {
                ++upload.NumberOfDownLoads;
                ufw.uploadRepository.Update(upload);
                await ufw.SaveChangesAsync();

                //Response.Headers.Add("Expires","0");
                //Response.Headers.Add("Cache-Control", "no-cache");  

                return File(upload.Url, upload.ContentType, upload.FileName);
            }
            return NotFound();
        }

    }
}
