using FileSharingApp.Areas.Admin.AdminRepositoryPattern.IRepository;
using FileSharingApp.Data;

namespace FileSharingApp.Areas.Admin.AdminRepositoryPattern.Repository
{
    public class AdminUnitOfWork:IAdminUnitOfWork
    {
        public IManagingUsers ManagingUsers { get; private set; }
        public IContactUs ContactUs { get; private set; }
        private readonly ApplicationDbContext db;
        public AdminUnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
            this.ManagingUsers = new ManagingUsers(db);
            this.ContactUs = new ContactUs(db);
        }
        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
