using FileSharingApp.Data;
using FileSharingApp.Models;
using FileSharingApp.RepositoryPattern.IRepository;

namespace FileSharingApp.RepositoryPattern
{
    public class ManagingUsers : SharedRepository<ApplicationUser>, IManagingUsers
    {
        private readonly ApplicationDbContext db;
        public ManagingUsers(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<bool> BlockUserByIdAsync(string id)
        {
            var user = await db.ApplicatioUsers.FindAsync(id);
            if (user is not null && !user.IsBlocked)
            {
                user.IsBlocked = true;
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public IQueryable<ApplicationUser> GetBlockedUsers()
        {
            var blockedUsers = db.ApplicatioUsers.Where(user => user.IsBlocked);
            return blockedUsers;
        }

        public async Task<bool> UnBlockUserByIdAsync(string id)
        {
            var user = await db.ApplicatioUsers.FindAsync(id);
            if (user is not null && user.IsBlocked)
            {
                user.IsBlocked = false;
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
