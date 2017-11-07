using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModel
{
    public class LoginViewModel
    {
        [Required, Display(Name = "Login Id")]
        public string LoginId { get; set; }

        [Required, Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string LoginKey { get; set; }
    }
}