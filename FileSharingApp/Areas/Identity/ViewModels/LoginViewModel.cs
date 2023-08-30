using FileSharingApp;
using FileSharingApp.Resources;
using System.ComponentModel.DataAnnotations;

namespace FileSharingApp.Areas.Identity.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources)), EmailAddress(ErrorMessageResourceName = "EmailAddress", ErrorMessageResourceType = typeof(SharedResources))]
        [Display(Name = nameof(Email), ResourceType = typeof(SharedResources))]
        public string Email { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources)), DataType(DataType.Password)]
        [Display(Name = nameof(Password), ResourceType = typeof(SharedResources))]
        public string Password { get; set; }

    }

}


