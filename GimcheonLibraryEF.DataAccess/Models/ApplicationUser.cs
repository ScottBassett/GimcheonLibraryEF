using Microsoft.AspNetCore.Identity;

namespace GimcheonLibraryEF.DataAccess.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string City { get; set; }
    }
}
