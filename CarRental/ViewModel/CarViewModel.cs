using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.ViewModel
{
    public class CarViewModel
    {
        [Key]
        public int? CarId { get; set; }

        [Required, Display(Name = "Branch")]
        public int? BranchId { get; set; }
        public string BranchName { get; set; }
        public SelectList BranchList { get; set; }

        [Required, Display(Name = "Type")]
        public string CarType { get; set; }
        public string CarTypeName { get; set; }
        public SelectList CarTypeList { get; set; }

        [Display(Name = "Fuel Type")]
        public string FuelTypeCode { get; set; }
        public string FuelTypeName { get; set; }
        public SelectList FuelTypeList { get; set; }

        [Display(Name = "Number")]
        public string CarNumber { get; set; }

        [Display(Name = "Driver")]
        public int? DriverId { get; set; }
        public string DriverName { get; set; }
        public SelectList DriverList { get; set; }

        [Display(Name = "Model Name")]
        public int? CarModelId { get; set; }
        public string ModelName { get; set; }
        public SelectList ModelList { get; set; }

        [Display(Name = "Registration No.")]
        public string RegistrationNumber { get; set; }

        [Display(Name = "Chasis No.")]
        public string ChasisNumber { get; set; }

        [Display(Name = "Engine No.")]
        public string EngineNumber { get; set; }

        [Display(Name = "Insurance Company")]
        public string InsuranceCompany { get; set; }

        [Display(Name = "Insurance Policy No.")]
        public string InsurancePolicyNo { get; set; }

        [Display(Name = "Insured From")]
        public DateTime? InsuranceStartDate { get; set; }

        [Display(Name = "Insured Upto")]
        public DateTime? InsuranceEndDate { get; set; }

        [Display(Name = "Tax From")]
        public DateTime? TaxStartDate { get; set; }

        [Display(Name = "Tax Upto")]
        public DateTime? TaxEndDate { get; set; }

        [Display(Name = "Authorisation From")]
        public DateTime? AuthorisationStartDate { get; set; }

        [Display(Name = "Authorisation Upto")]
        public DateTime? AuthorisationEndDate { get; set; }

        [Display(Name = "Fitness From")]
        public DateTime? FitnessStartDate { get; set; }

        [Display(Name = "Fitness Upto")]
        public DateTime? FitnessEndDate { get; set; }

        [Display(Name = "Permit From")]
        public DateTime? PermitStartDate { get; set; }

        [Display(Name = "Permit Upto")]
        public DateTime? PermitEndDate { get; set; }

        [Display(Name = "P.U.C. From")]
        public DateTime? PucStartDate { get; set; }

        [Display(Name = "P.U.C. Upto")]
        public DateTime? PucEndDate { get; set; }

        [Display(Name = "Finance By")]
        public string FinanceBy { get; set; }

        [Display(Name = "Owner")]
        public int? OwnerId { get; set; }
        public string OwnerName { get; set; }
        public SelectList OwnerList { get; set; }

        [Display(Name = "Car In Use")]
        public bool CarInUse { get; set; }

        [Display(Name = "Car On Hold")]
        public bool CarOnHold { get; set; }

        [Display(Name = "Servicing Slab")]
        public int? ServicingSlab { get; set; }

        [Display(Name = "E.M.I. Amount")]
        public decimal? EmiAmount { get; set; }

        [Display(Name = "E.M.I. Date")]
        public DateTime? EmiDate { get; set; }
    }
}