using AutoMapper;
using FileSharingApp.Models;
using System.Security.Claims;

namespace FileSharingApp.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactViewModel, Contact>();
        }
    }
}
