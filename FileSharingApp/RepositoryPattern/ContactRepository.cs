using FileSharingApp.Data;
using FileSharingApp.Models;
using FileSharingApp.RepositoryPattern.IRepository;

namespace FileSharingApp.RepositoryPattern
{
    public class ContactRepository : SharedRepository<Contact>, IContactRepository
    {
        private readonly ApplicationDbContext db;

        public ContactRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }
    }
}
