using FileSharingApp.Resources;
using System.ComponentModel.DataAnnotations;

namespace FileSharingApp.Areas.Identity.Models
{
    public class AddPasswordViewModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources)), DataType(DataType.Password)]
        [Display(Name = nameof(Password), ResourceType = typeof(SharedResources))]
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessageResourceName = "Compare", ErrorMessageResourceType = typeof(SharedResources)), DataType(DataType.Password)]
        [Display(Name = nameof(ConfirmPassword), ResourceType = typeof(SharedResources))]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
        public string UserId { get; set; }

    }
}
