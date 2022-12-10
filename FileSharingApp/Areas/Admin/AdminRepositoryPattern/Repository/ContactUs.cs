using FileSharingApp.Areas.Admin.AdminRepositoryPattern.IRepository;
using FileSharingApp.Data;
using FileSharingApp.SharedData;
using FileSharingApp.SharedRepositoryPattern.Repository;
namespace FileSharingApp.Areas.Admin.AdminRepositoryPattern.Repository
{
    public class ContactUs:SharedRepository<Contact>,IContactUs
    {
        private readonly ApplicationDbContext db;
        public ContactUs(ApplicationDbContext db) : base(db) {
            this.db = db;
        }

    }
}
