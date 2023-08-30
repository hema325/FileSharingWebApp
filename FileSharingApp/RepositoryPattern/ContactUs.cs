using FileSharingApp.Data;
using FileSharingApp.Models;
using FileSharingApp.RepositoryPattern.IRepository;

namespace FileSharingApp.RepositoryPattern
{
    public class ContactUs : SharedRepository<Contact>, IContactUs
    {
        private readonly ApplicationDbContext db;
        public ContactUs(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

    }
}
