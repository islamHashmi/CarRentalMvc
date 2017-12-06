using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModel
{
    public class DutySlipViewModel
    {
        [Key]
        public long? DutySlipId { get; set; }

        [Required, Display(Name = "Branch")]
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public SelectList BranchList { get; set; }

        [Required, Display(Name = "Slip Number")]
        public int? SlipNumber { get; set; }

        [Required, Display(Name = "Slip Date")]
        public DateTime? SlipDate { get; set; }

        [Display(Name = "Company")]
        public int PartyId { get; set; }
        public string PartyName { get; set; }
        public SelectList PartyList { get; set; }

        [Display(Name = "Booking Number")]
        public long? BookingId { get; set; }
        public string BookingNumber { get; set; }
        public SelectList BookingNoList { get; set; }

        [Display(Name = "Type")]
        public string CarType { get; set; }
        public string CarTypeName { get; set; }
        
        [Display(Name = "Vehicle")]
        public int? CarId { get; set; }
        public string CarNo { get; set; }
        public SelectList CarNumberList { get; set; }

        [Display(Name = "Car Number")]
        public string OP_CarNumber { get; set; }

        [Display(Name = "Registration No.")]
        public string OP_CarRegisterNumber { get; set; }

        [Display(Name = "Driver Name")]
        public string OP_DriverName { get; set; }

        [Display(Name = "Driver Mobile")]
        public string OP_DriverMobile { get; set; }

        [Display(Name = "Supplier")]
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public SelectList SupplierList { get; set; }

        [Display(Name = "Driver")]
        public int? DriverId { get; set; }
        public string DriverName { get; set; }
        public SelectList DriverNameList { get; set; }

        [Display(Name = "Driver Mobile")]
        public string DriverMobileNo { get; set; }

        [Display(Name = "Balance")]
        public decimal? DriverBalance { get; set; }

        [Display(Name = "Driver Advance")]
        public decimal? AdvanceDriver { get; set; }

        [Display(Name = "Opening Time")]
        public TimeSpan? OpeningTime { get; set; }

        [Display(Name = "Opening K.M.")]
        public decimal? OpeningKM { get; set; }

        [Display(Name = "Booking Type")]
        public string BookingType { get; set; }

        [Display(Name = "Booking By")]
        public string BookingBy { get; set; }

        [Display(Name = "Reporting To")]
        public string ReportingTo { get; set; }

        [Display(Name = "Reporting Location")]
        public string ReportingLocation { get; set; }

        [Display(Name = "Reporting Time")]
        public TimeSpan? ReportingTime { get; set; }

        [Display(Name = "Model")]
        public string CarModel { get; set; }

        [Display(Name = "Duty Type")]
        public string DutyType { get; set; }

        [Display(Name = "Duty Date From")]
        public DateTime? DutyDateFrom { get; set; }

        [Display(Name = "Duty Date To")]
        public DateTime? DutyDateTo { get; set; }        

        [Display(Name = "Mobile 1")]
        public string Mobile1 { get; set; }

        [Display(Name = "Mobile 2")]
        public string Mobile2 { get; set; }

        [Display(Name = "Mobile 3")]
        public string Mobile3 { get; set; }
    }

    public class DutySlipFilter
    {
        public IEnumerable<DutySlipViewModel> DutySlipList { get; set; }
    }
}