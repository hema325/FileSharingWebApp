using FileSharingApp.Data;
using FileSharingApp.Models;
using FileSharingApp.RepositoryPattern.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FileSharingApp.RepositoryPattern
{
    public class UploadRepository : SharedRepository<Upload>, IUploadRepository
    {
        private readonly ApplicationDbContext db;
        public UploadRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

    }
}
