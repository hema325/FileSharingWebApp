using FileSharingApp.Constraints;
using FileSharingApp.Data;
using FileSharingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FileSharingApp.DataBaseInitializer
{
    public class DbInitializer :IDbInitializer
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public DbInitializer(ApplicationDbContext db,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task Initialize()
        {
            if(db.Database.GetPendingMigrations().Count() != 0)
               db.Database.Migrate();

            if(!await roleManager.RoleExistsAsync(IdentityRoles.Admin)) 
            {
                await roleManager.CreateAsync(new IdentityRole(IdentityRoles.Admin));
                await roleManager.CreateAsync(new IdentityRole(IdentityRoles.User));

                var admin = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(admin, "Hema123!");
                await userManager.AddToRoleAsync(admin, IdentityRoles.Admin);
            }

        }
    }
}
