
using FileSharingApp.Resources;
using System.ComponentModel.DataAnnotations;

namespace FileSharingApp.Areas.Identity.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources)), DataType(DataType.Password)]
        [Display(Name = nameof(OldPassword), ResourceType = typeof(SharedResources))]
        public string OldPassword { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources)), DataType(DataType.Password)]
        [Display(Name = nameof(NewPassword), ResourceType = typeof(SharedResources))]
        public string NewPassword { get; set; }
        [Compare(nameof(NewPassword), ErrorMessageResourceName = "Compare", ErrorMessageResourceType = typeof(SharedResources)), DataType(DataType.Password)]
        [Display(Name = nameof(ConfirmNewPassword), ResourceType = typeof(SharedResources))]
        public string ConfirmNewPassword { get; set; }
    }
}
