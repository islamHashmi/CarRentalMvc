using System;
using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModel
{
    public class RegisterViewModel
    {
        [Key]
        public int? UserId { get; set; }

        [Required, Display(Name = "Employee Name")]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public System.Web.Mvc.SelectList EmployeeList { get; set; }

        [Required, Display(Name = "Login Id")]
        public string LoginId { get; set; }

        [Required, Display(Name = "Password")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string LoginKey { get; set; }

        [Required, Display(Name = "Confirm Password")]
        [Compare("LoginKey", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmLoginKey { get; set; }

        [Display(Name = "OTP Reqiured")]
        public bool OtpRequired { get; set; }

        [Display(Name = "Last Logged In")]
        public DateTime? LastLogin { get; set; }
    }
}