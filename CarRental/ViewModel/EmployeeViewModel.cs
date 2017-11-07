using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CarRental.ViewModel
{
    public class EmployeeViewModel
    {
        [Key]
        public int? EmployeeId { get; set; }

        [Display(Name ="Employee Code")]
        public string EmployeeCode { get; set; }

        [Required, Display(Name = "Branch")]
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public SelectList BranchList { get; set; }

        [Required, Display(Name = "Designation")]
        public int? DesignationId { get; set; }
        public string DesignationName { get; set; }
        public SelectList DesignationList { get; set; }

        [Required,Display(Name ="Employee Name")]
        public string EmployeeName { get; set; }

        [Required, Display(Name = "Residentail Address")]
        public string ResidentiallAddress { get; set; }

        [Required, Display(Name = "Native Address")]
        public string NativeAddress { get; set; }

        [Required, Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Required, Display(Name = "Residential Tel.")]
        public string ResidentialTelephone { get; set; }

        [Required, Display(Name = "Joining Date")]
        public DateTime? JoiningDate { get; set; }

        [Required, Display(Name = "Leaving Date")]
        public DateTime? LeavingDate { get; set; }

        [Required, Display(Name = "Basic Amount")]
        public decimal? BasicAmount { get; set; }

        [Required, Display(Name = "HRA Amount")]
        public decimal? HraAmount { get; set; }

        [Required, Display(Name = "CC Amount")]
        public decimal? CcAmount { get; set; }

        [Required, Display(Name = "Bank")]
        public bool Bank { get; set; }

        [Required, Display(Name = "Account Number")]
        public string AccountNumber { get; set; }        
    }
}