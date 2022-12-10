using FileSharingApp.Data;
using FileSharingApp.RepositoryPattern.IRepository;
using FileSharingApp.SharedData;
using FileSharingApp.SharedRepositoryPattern.Repository;

namespace FileSharingApp.RepositoryPattern.Repository
{
    public class ContactRepository: SharedRepository<Contact>,IContactRepository
    {
        private readonly ApplicationDbContext db;

        public ContactRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }
    }
}
