using Microsoft.AspNetCore.Identity;

namespace TomasosPizzeria.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
    }
}
