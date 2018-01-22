using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.Models.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required, MaxLength(20), Display(Name = "Användarnamn")]
        public string UserName { get; set; }
        [Required, MaxLength(100), Display(Name ="För och efternamn")]
        public string FullName { get; set; }
        [Required, EmailAddress, MaxLength(50), Display(Name = "E-post")]
        public string Email { get; set; }
        [Required, MinLength(6), MaxLength(20), DataType(DataType.Password), Display(Name = "Lösenord")]
        public string Password { get; set; }
        [MaxLength(50), Display(Name = "Gatuadress")]
        public string Adress { get; set; }
        [MaxLength(20), Display(Name = "Postnr")]
        public string PostalCode { get; set; }
        [MaxLength(100), Display(Name = "Postort")]
        public string City { get; set; }
        [MaxLength(50), Display(Name = "Telefonnummer")]
        public string Phone { get; set; }

    }
}
