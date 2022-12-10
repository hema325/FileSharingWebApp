using FileSharingApp.Areas.Admin.AdminRepositoryPattern.Repository;
using FileSharingApp.Areas.Identity.Data;
using FileSharingApp.SharedRepositoryPattern.IRepository;

namespace FileSharingApp.Areas.Admin.AdminRepositoryPattern.IRepository
{
    public interface IManagingUsers:ISharedRepository<ApplicationUser>
    {
        Task<bool> BlockUserByIdAsync(string id);
        Task<bool> UnBlockUserByIdAsync(string id);
    }
}
