using FileSharingApp.RepositoryPattern.IRepository;
using FileSharingApp.RepositoryPattern.Repository;

namespace FileSharingApp.Areas.Users
{
    public static class UsersStartup
    {
        public static IServiceCollection AddUsersServices(this IServiceCollection Services)
        {
            Services.AddScoped<IUsersUnitOfWork, UsersUnitOfWork>();

            return Services;
        }
    }
}
