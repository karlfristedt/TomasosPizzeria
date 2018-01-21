using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.Models.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required, MaxLength(256), Display(Name = "Användarnamn")]
        public string UserName { get; set; }
        [Required, MaxLength(100), Display(Name ="För och efternamn")]
        public string FullName { get; set; }
        [Required, EmailAddress, MaxLength(256), Display(Name = "E-post")]
        public string Email { get; set; }
        [Required, MinLength(6), MaxLength(20), DataType(DataType.Password), Display(Name = "Lösenord")]
        public string Password { get; set; }

    }
}
