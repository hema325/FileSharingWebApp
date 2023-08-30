using FileSharingApp;
using FileSharingApp.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FileSharingApp.Areas.Identity.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources)), EmailAddress(ErrorMessageResourceName = "EmailAddress", ErrorMessageResourceType = typeof(SharedResources))]
        [Display(Name = nameof(Email), ResourceType = typeof(SharedResources))]
        public string Email { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources)), DataType(DataType.Password)]
        [Display(Name = nameof(Password), ResourceType = typeof(SharedResources))]
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessageResourceName = "Compare", ErrorMessageResourceType = typeof(SharedResources)), DataType(DataType.Password)]
        [Display(Name = nameof(ConfirmPassword), ResourceType = typeof(SharedResources))]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources)), StringLength(20)]
        [Display(Name = nameof(FirstName), ResourceType = typeof(SharedResources))]
        public string FirstName { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(SharedResources)), StringLength(20)]
        [Display(Name = nameof(LastName), ResourceType = typeof(SharedResources))]
        public string LastName { get; set; }

        public string Role { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }

    }
}
