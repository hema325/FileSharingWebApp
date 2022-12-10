using FileSharingApp.Resources;
using System.ComponentModel.DataAnnotations;

namespace FileSharingApp.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources))]
        [Display(Name = nameof(Name), ResourceType = typeof(SharedResources))]
        public string Name { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources)), EmailAddress(ErrorMessageResourceName = "EmailAddress", ErrorMessageResourceType = typeof(SharedResources))]
        [Display(Name = nameof(Email), ResourceType = typeof(SharedResources))]
        public string Email { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources))]
        [Display(Name = nameof(Subject), ResourceType = typeof(SharedResources))]
        public string Subject { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources))]
        [Display(Name = nameof(Message), ResourceType = typeof(SharedResources))]
        public string Message { get; set; }
    }
}
