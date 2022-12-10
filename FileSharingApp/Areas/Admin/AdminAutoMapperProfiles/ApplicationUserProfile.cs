using AutoMapper;
using FileSharingApp.Areas.Admin.Models;
using FileSharingApp.Areas.Identity.Data;

namespace FileSharingApp.Areas.Admin.AdminAutoMapperProfiles
{
    public class ApplicationUserProfile:Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, UserAdminViewModel>();
        }
    }
}
