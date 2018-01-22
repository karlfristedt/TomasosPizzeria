using System.ComponentModel.DataAnnotations;

namespace TomasosPizzeria.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required, MaxLength(20), Display(Name = "Användarnamn")]
        public string UserName { get; set; }
        [Required, MinLength(6), MaxLength(20), DataType(DataType.Password), Display(Name = "Lösenord")]
        public string Password { get; set; }
    }
}
