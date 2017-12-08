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

        [Display(Name = "Opening Time")]
        public TimeSpan? OpeningTime { get; set; }

        [Display(Name = "Closing Time")]
        public TimeSpan? ClosingTime { get; set; }

        [Display(Name = "Total Time")]
        public decimal? TotalTime { get; set; }

        [Display(Name = "Opening K.M.")]
        public decimal? OpeningKM { get; set; }

        [Display(Name = "Closing K.M.")]
        public decimal? ClosingKM { get; set; }

        [Display(Name = "Total K.M.")]
        public decimal? TotalKM { get; set; }

        [Display(Name = "Release Point")]
        public int? ReleasePointId { get; set; }
        public string ReleasePointName { get; set; }
        public SelectList ReleasePointList { get; set; }

        [Display(Name = "Billing Type")]
        public string BillingType { get; set; }

        [Display(Name = "Parking Charge")]
        public decimal? ParkingCharge { get; set; }

        [Display(Name = "Fuel Charge")]
        public decimal? FuelCharge { get; set; }

        [Display(Name = "Other Charge")]
        public decimal? OtherCharge { get; set; }

        [Display(Name = "Extra Charge")]
        public string ExtraChargeName { get; set; }

        [Display(Name = "Extra Amount")]
        public decimal? ExtraAmount { get; set; }

        [Display(Name = "Paid Amount")]
        public decimal? PaidAmount { get; set; }

        [Display(Name = "Received Amount")]
        public decimal? ReceivedAmount { get; set; }

        [Display(Name = "Advance Taken")]
        public decimal? AdvanceTaken { get; set; }

        [Display(Name = "Route")]
        public string Route { get; set; }

        [Display(Name = "Payable to driver")]
        public bool PayableToDriver { get; set; }

        [Display(Name ="Scheme")]
        public int SchemeId { get; set; }
        public string SchemeName { get; set; }
        public SelectList SchemeList { get; set; }

        [Display(Name = "Bill Opening Time")]
        public TimeSpan? BillOpeningTime { get; set; }

        [Display(Name = "Bill Closing Time")]
        public TimeSpan? BillClosingTime { get; set; }

        [Display(Name = "Bill Opening KM")]
        public decimal? BillOpeningKm { get; set; }

        [Display(Name = "Bill Closing KM")]
        public decimal? BillClosingKm { get; set; }

        [Display(Name = "Day Allowance")]
        public decimal DayAllowance { get; set; }

        [Display(Name = "Night Allowance")]
        public decimal? NightAllowance { get; set; }

        [Display(Name = "Total KM Amount")]
        public decimal? TotalKmAmount { get; set; }

        [Display(Name = "Ex. KM")]
        public decimal? ExtraKm { get; set; }

        [Display(Name = "Ex. KM Amount")]
        public decimal? ExtraKmAmount { get; set; }

        [Display(Name = "Ex. Hours")]
        public decimal? ExtraHours { get; set; }

        [Display(Name = "Ex. Hour Amount")]
        public decimal? ExtraHoursAmount { get; set; }

        [Display(Name = "Total Amount")]
        public decimal? TotalAmount { get; set; }
    }

    public class DutySlipFilter
    {
        public IEnumerable<DutySlipViewModel> DutySlipList { get; set; }
    }
}