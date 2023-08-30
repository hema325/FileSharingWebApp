using AutoMapper;
using FileSharingApp.Models;

namespace FileSharingApp.Profiles
{
    public class UploadProfile : Profile
    {
        public UploadProfile()
        {
            CreateMap<Upload, ListUploadViewModel>();

        }
    }
}
