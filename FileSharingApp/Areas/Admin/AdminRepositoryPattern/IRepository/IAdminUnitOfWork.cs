namespace FileSharingApp.Areas.Admin.AdminRepositoryPattern.IRepository
{
    public interface IAdminUnitOfWork
    {
        IManagingUsers ManagingUsers { get; }
        IContactUs ContactUs { get; }

        Task SaveChangesAsync();
    }
}
