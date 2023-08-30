using FileSharingApp.Data;
using FileSharingApp.SharedRepositoryPattern.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FileSharingApp.RepositoryPattern
{
    public class SharedRepository<T> : ISharedRepository<T> where T : class
    {
        private readonly DbSet<T> dbSet;
        public SharedRepository(ApplicationDbContext db)
        {
            dbSet = db.Set<T>();
        }

        public async Task<T> FindAsync(Guid Id)
        {
            return await dbSet.FindAsync(Id);
        }

        public async Task<T> FindAsync(int Id)
        {
            return await dbSet.FindAsync(Id);
        }

        public async Task AddAsync(T upload)
        {
            await dbSet.AddAsync(upload);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, string path)
        {
            return await dbSet.Include(path).FirstOrDefaultAsync(predicate);

        }

        public IQueryable<T> GetAll(string path)
        {
            return dbSet.Include(path);
        }

        public void Remove(T upload)
        {
            dbSet.Remove(upload);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public void Update(T upload)
        {
            dbSet.Update(upload);
        }

        public async Task<int> CountAsync()
        {
            return await dbSet.CountAsync();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.CountAsync(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate, string path)
        {
            return dbSet.Where(predicate).Include(path);
        }
    }
}
