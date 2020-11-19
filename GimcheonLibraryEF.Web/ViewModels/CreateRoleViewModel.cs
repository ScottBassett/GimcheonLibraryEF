using System.ComponentModel.DataAnnotations;

namespace GimcheonLibraryEF.Web.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
