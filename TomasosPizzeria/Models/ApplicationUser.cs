using Microsoft.AspNetCore.Identity;

namespace TomasosPizzeria.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
