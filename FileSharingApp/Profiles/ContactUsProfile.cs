using AutoMapper;
using FileSharingApp.Areas.Admin.Models;
using FileSharingApp.Models;

namespace FileSharingApp.Profiles
{
    public class ContactUsProfile : Profile
    {
        public ContactUsProfile()
        {
            CreateMap<Contact, ContactUsAdminViewModel>().ForMember(destination => destination.UserName, options => options.MapFrom(source => $"{source.User.FirstName} {source.User.LastName}"));
        }
    }
}
