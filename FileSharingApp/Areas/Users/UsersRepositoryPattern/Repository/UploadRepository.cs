using FileSharingApp.Areas.Users.Data;
using FileSharingApp.Data;
using FileSharingApp.RepositoryPattern.IRepository;
using FileSharingApp.SharedRepositoryPattern.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FileSharingApp.RepositoryPattern.Repository
{
    public class UploadRepository: SharedRepository<Upload>,IUploadRepository
    {
        private readonly ApplicationDbContext db;
        public UploadRepository(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }

    }
}
