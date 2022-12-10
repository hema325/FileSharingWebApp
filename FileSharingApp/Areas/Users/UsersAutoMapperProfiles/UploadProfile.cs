using AutoMapper;
using FileSharingApp.Areas.Users.Data;
using FileSharingApp.Models;

namespace FileSharingApp.AutoMapperProfiles
{
    public class UploadProfile:Profile
    {
        public UploadProfile()
        {
            CreateMap<Upload, ListUploadViewModel>();
           
        }
    }
}
