using AutoMapper;
using FileSharingApp.Models;
using FileSharingApp.SharedData;
using System.Security.Claims;

namespace FileSharingApp.AutoMapperProfiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactViewModel, Contact>();
        }
    }
}
