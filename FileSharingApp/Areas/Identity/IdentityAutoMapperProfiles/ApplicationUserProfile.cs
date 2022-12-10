using AutoMapper;
using FileSharingApp.Areas.Identity.Data;
using FileSharingApp.Areas.Identity.Models;

namespace FileSharingApp.Areas.Identity.IdentityAutoMapperProfiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<RegisterViewModel, ApplicationUser>().ForMember(user => user.UserName, op => op.MapFrom(model => model.Email));
            CreateMap<ApplicationUser, RegisterViewModel>();
            CreateMap<ChangeUserInformationViewModel, ApplicationUser>();
            CreateMap<ApplicationUser, ChangeUserInformationViewModel>().ForMember(des => des.HasPassword, op => op.MapFrom(user => user.PasswordHash != null));
        }
    }
}
