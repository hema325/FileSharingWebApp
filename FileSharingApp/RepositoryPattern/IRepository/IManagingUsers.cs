using FileSharingApp.Models;
using FileSharingApp.SharedRepositoryPattern.IRepository;

namespace FileSharingApp.RepositoryPattern.IRepository
{
    public interface IManagingUsers : ISharedRepository<ApplicationUser>
    {
        Task<bool> BlockUserByIdAsync(string id);
        Task<bool> UnBlockUserByIdAsync(string id);
    }
}
