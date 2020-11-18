using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace GimcheonLibraryEF.Web.ViewModels
{
    public class UserCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public IFormFile Photo { get; set; }
    }
}
