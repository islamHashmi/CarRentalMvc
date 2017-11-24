using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CarRental.ViewModel
{
    public class EmployeeViewModel
    {
        [Key]
        public int? EmployeeId { get; set; }

        [Display(Name ="ID Name")]
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

        [Display(Name = "Native Address")]
        public string NativeAddress { get; set; }

        [Required, Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Display(Name = "Residential Tel.")]
        public string ResidentialTelephone { get; set; }

        [Required, Display(Name = "Joining Date")]
        public DateTime? JoiningDate { get; set; }

        [Display(Name = "Leaving Date")]
        public DateTime? LeavingDate { get; set; }

        [Display(Name = "Basic Amount")]
        public decimal? BasicAmount { get; set; }

        [Display(Name = "HRA Amount")]
        public decimal? HraAmount { get; set; }

        [Display(Name = "CC Amount")]
        public decimal? CcAmount { get; set; }
        
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }

        [Display(Name = "License No.")]
        public string LicenseNumber { get; set; }

        [Display(Name = "O.T. Rate/Hr.")]
        public decimal? OtRatePerHour { get; set; }

        [Display(Name = "Outstation 150")]
        public decimal? Outstation150 { get; set; }

        [Display(Name = "Outstation 100")]
        public decimal? Outstation100 { get; set; }

        [Display(Name = "P.F. %")]
        public decimal? PfAmount { get; set; }

        [Display(Name = "Extra Duty")]
        public decimal? ExtraDuty { get; set; }

        [Display(Name = "Sunday")]
        public decimal? SundayAmount { get; set; }

    }
}