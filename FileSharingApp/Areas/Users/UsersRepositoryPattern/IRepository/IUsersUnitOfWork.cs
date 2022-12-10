namespace FileSharingApp.RepositoryPattern.IRepository
{
    public interface IUsersUnitOfWork
    {
        IUploadRepository uploadRepository { get; }
        IContactRepository contactRepository { get; }

        Task SaveChangesAsync();
    }
}
