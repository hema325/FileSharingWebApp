using FileSharingApp.Data;
using FileSharingApp.RepositoryPattern.IRepository;

namespace FileSharingApp.RepositoryPattern.Repository
{
    public class UsersUnitOfWork : IUsersUnitOfWork
    {
        private readonly ApplicationDbContext db;
        public IUploadRepository uploadRepository{get;private set;}
        public IContactRepository contactRepository { get; private set; }
        public UsersUnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
            uploadRepository = new UploadRepository(db);
            contactRepository = new ContactRepository(db);
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }

    }
}
