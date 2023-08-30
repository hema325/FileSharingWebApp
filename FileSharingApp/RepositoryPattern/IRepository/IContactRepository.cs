using FileSharingApp.Models;
using FileSharingApp.SharedRepositoryPattern.IRepository;

namespace FileSharingApp.RepositoryPattern.IRepository
{
    public interface IContactRepository:ISharedRepository<Contact>
    {
    }
}
