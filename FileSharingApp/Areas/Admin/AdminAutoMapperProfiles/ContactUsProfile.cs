using AutoMapper;
using FileSharingApp.Areas.Admin.Models;
using FileSharingApp.SharedData;

namespace FileSharingApp.Areas.Admin.AdminAutoMapperProfiles
{
    public class ContactUsProfile:Profile
    {
        public ContactUsProfile()
        {
            CreateMap<Contact, ContactUsAdminViewModel>().ForMember(destination=>destination.UserName,options=>options.MapFrom(source => $"{source.User.FirstName} {source.User.LastName}"));
        }
    }
}
