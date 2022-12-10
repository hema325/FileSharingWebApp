using FileSharingApp.Areas.Identity.Data;
using FileSharingApp.Areas.Users.Data;
using FileSharingApp.SharedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FileSharingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext <IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Upload> Uploads { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ApplicationUser> ApplicatioUsers { get; set; }

    }
}
