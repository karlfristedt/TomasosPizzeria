using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TomasosPizzeria.Models.ViewModels
{
    public class EditUserViewModel
    {
        [Required, MaxLength(20), Display(Name = "Användarnamn")]
        public string UserName { get; set; }
        [Required, MaxLength(100), Display(Name = "För och efternamn")]
        public string FullName { get; set; }
        [Required, EmailAddress, MaxLength(50), Display(Name = "E-post")]
        public string Email { get; set; }
        [MinLength(6), MaxLength(20), DataType(DataType.Password), Display(Name = "Nytt lösenord")]
        public string NewPassword { get; set; }
        [MaxLength(50), Display(Name = "Gatuadress")]
        public string Adress { get; set; }
        [MaxLength(20), Display(Name = "Postnr")]
        public string PostalCode { get; set; }
        [MaxLength(100), Display(Name = "Postort")]
        public string City { get; set; }
        [MaxLength(50), Display(Name = "Telefonnummer")]
        public string Phone { get; set; }

        [MinLength(6), MaxLength(20), DataType(DataType.Password), Display(Name = "Nuvarande lösenord")]
        public string CurrentPassword { get; set; }

        public RoleManager<IdentityUser> Roll { get; set; }
    }
}
