using FileSharingApp.Areas.Admin.AdminRepositoryPattern.IRepository;
using FileSharingApp.Areas.Admin.AdminRepositoryPattern.Repository;

namespace FileSharingApp.Areas.Admin
{
    public static class AdminStartup
    {
        public static IServiceCollection AddAdminServices(this IServiceCollection Services)
        {
            Services.AddScoped<IAdminUnitOfWork, AdminUnitOfWork>();

            return Services;
        }
    }
}
