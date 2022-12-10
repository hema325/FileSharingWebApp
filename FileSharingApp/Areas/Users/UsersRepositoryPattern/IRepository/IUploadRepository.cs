using FileSharingApp.Areas.Users.Data;
using FileSharingApp.SharedRepositoryPattern.IRepository;
using System.Linq.Expressions;

namespace FileSharingApp.RepositoryPattern.IRepository
{
    public interface IUploadRepository : ISharedRepository<Upload>
    {
    }
}
