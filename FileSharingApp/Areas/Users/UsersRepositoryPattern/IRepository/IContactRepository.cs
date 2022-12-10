using FileSharingApp.SharedData;
using FileSharingApp.SharedRepositoryPattern.IRepository;

namespace FileSharingApp.RepositoryPattern.IRepository
{
    public interface IContactRepository:ISharedRepository<Contact>
    {
    }
}
