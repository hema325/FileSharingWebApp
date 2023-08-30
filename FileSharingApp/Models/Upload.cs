using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FileSharingApp.Models
{
    public class Upload
    {
        public Guid Id { get; set; }
        [StringLength(maximumLength: 256)]
        public string FileName { get; set; }
        [StringLength(maximumLength: 256)]
        public string Url { get; set; }

        [Precision(precision: 18, scale: 2)]
        public decimal FileSize { get; set; }
        [StringLength(maximumLength: 150)]
        public string ContentType { get; set; }
        public DateTime UploadedDateTime { get; set; }
        public long NumberOfDownLoads { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

    }
}
