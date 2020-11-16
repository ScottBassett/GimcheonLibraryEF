using Microsoft.AspNet.Identity.EntityFramework;

namespace GimcheonLibraryEF.DataAccess.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }
        public AppRole(string name) : base(name) { }
    }
}
