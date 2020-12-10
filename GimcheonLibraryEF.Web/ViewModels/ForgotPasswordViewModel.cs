using System.ComponentModel.DataAnnotations;

namespace GimcheonLibraryEF.Web.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
