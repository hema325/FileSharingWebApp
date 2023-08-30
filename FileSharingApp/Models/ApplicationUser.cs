using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FileSharingApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, StringLength(20)]
        public string FirstName { get; set; }
        [Required, StringLength(20)]
        public string LastName { get; set; }
        [Required]
        public bool IsBlocked { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

    }
}
