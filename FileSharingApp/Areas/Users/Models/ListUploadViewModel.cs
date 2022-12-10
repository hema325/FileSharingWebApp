using System.ComponentModel.DataAnnotations;

namespace FileSharingApp.Models
{
    public class ListUploadViewModel
    {
        public Guid Id { get; set; }
        [Display(Name ="Name")]
        public string FileName { get; set; }
        [Display(Name ="Size")]
        public decimal FileSize { get; set; }
        [Display(Name ="Type")]
        public string ContentType { get; set; }
        [Display(Name ="Date")]
        public DateTime UploadedDateTime { get; set; }
        [Display(Name ="Downloads")]
        public long NumberOfDownLoads { get; set; }
        public string Url { get; set; }

    }
}
