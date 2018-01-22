using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.Models.ViewModels
{
    public class UsersViewModel
    {
        public ICollection<ApplicationUser> RegularUsers { get; set; }
        public ICollection<ApplicationUser> PremiumUsers { get; set; }
    }
}
