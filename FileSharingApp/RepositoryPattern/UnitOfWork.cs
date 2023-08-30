using FileSharingApp.Data;
using FileSharingApp.RepositoryPattern.IRepository;

namespace FileSharingApp.RepositoryPattern
{
    public class UnitOfWork: IUnitOfWork
    {
        public IManagingUsers ManagingUsers { get; private set; }
        public IContactUs ContactUs { get; private set; }
        public IUploadRepository uploadRepository { get; private set; }
        public IContactRepository contactRepository { get; private set; }

        private readonly ApplicationDbContext db;
        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
            this.ManagingUsers = new ManagingUsers(db);
            this.ContactUs = new ContactUs(db);
            uploadRepository = new UploadRepository(db);
            contactRepository = new ContactRepository(db);
        }
        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
