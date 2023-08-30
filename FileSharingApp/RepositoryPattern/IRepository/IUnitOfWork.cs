namespace FileSharingApp.RepositoryPattern.IRepository
{
    public interface IUnitOfWork
    {
        IUploadRepository uploadRepository { get; }
        IContactRepository contactRepository { get; }
        IManagingUsers ManagingUsers { get; }
        IContactUs ContactUs { get; }
        Task SaveChangesAsync();
    }
}
