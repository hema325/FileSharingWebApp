using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FileSharingApp.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }
        [Required, StringLength(100)]
        public string Subject { get; set; }
        [Required, StringLength(500)]
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime SendDate { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
