using FileSharingApp;
using FileSharingApp.Resources;
using System.ComponentModel.DataAnnotations;

namespace FileSharingApp.Areas.Identity.Models
{
    public class ChangeUserInformationViewModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources)), StringLength(20)]
        [Display(Name = nameof(FirstName), ResourceType = typeof(SharedResources))]
        public string FirstName { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources)), StringLength(20)]
        [Display(Name = nameof(LastName), ResourceType = typeof(SharedResources))]
        public string LastName { get; set; }
        public bool HasPassword { get; set; }
        public ChangePasswordViewModel changePasswordViewModel { get; set; }
        public AddPasswordViewModel addPasswordViewModel { get; set; }
    }
}
