using System.Linq.Expressions;

namespace FileSharingApp.SharedRepositoryPattern.IRepository
{
    public interface ISharedRepository<T> where T : class
    {
        Task<T> FindAsync(Guid Id);
        Task<T> FindAsync(int Id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(string path);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, string path);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);

        IQueryable<T> Where(Expression<Func<T, bool>> predicate,string path);
        void Remove(T upload);
        Task AddAsync(T upload);

        void Update(T upload);

        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T,bool>> predicate);
    }
}
