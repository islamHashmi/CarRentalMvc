using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CarRental.ViewModel
{
    public class DriverAllocationViewModel
    {
        [Key]
        public int? AllocationId { get; set; }

        [Required, Display(Name = "Booking Number")]
        public long? BookingId { get; set; }
        public string BookingNumber { get; set; }
        public SelectList BookingNoList { get; set; }

        [Required, Display(Name = "Type")]
        public string CarType { get; set; }

        [Display(Name = "Car Number")]
        public int? CarId { get; set; }
        public SelectList CarNumberList { get; set; }

        [Display(Name = "Car Number")]
        public string CarNumber { get; set; }

        [Display(Name = "Car Reg. Number")]
        public string CarRegisterNumber { get; set; }

        [Display(Name = "Driver Name")]
        public int? DriverId { get; set; }
        public SelectList DriverNameList { get; set; }

        [Display(Name = "Driver Name")]
        public string DriverName { get; set; }

        [Display(Name = "Driver Mobile")]
        public string DriverMobileNo { get; set; }

        [Display(Name = "Slip No.")]
        public int? SlipNumber { get; set; }

        [Display(Name ="Booking Date")]
        public DateTime? BookingDate { get; set; }

        [Display(Name = "Booking Received By")]
        public string BookingReceivedBy { get; set; }

        [Display(Name = "Company")]
        public string Company { get; set; }

        [Display(Name = "Booking By")]
        public string BookingBy { get; set; }

        [Display(Name = "Booking Source")]
        public string BookingSource { get; set; }

        [Display(Name = "Source Detail")]
        public string SourceDetail { get; set; }

        [Display(Name = "Date From")]
        public DateTime? DateFrom { get; set; }

        [Display(Name = "Date To")]
        public DateTime? DateTo { get; set; }

        [Display(Name = "Reporting To")]
        public string ReportingTo { get; set; }

        [Display(Name = "Reporting Location")]
        public string ReportingLocation { get; set; }

        [Display(Name = "Reporting Time")]
        public TimeSpan? ReportingTime { get; set; }

        [Display(Name = "Model")]
        public string ModelCar { get; set; }

        [Display(Name = "Duty Type")]
        public string DutyType { get; set; }

        [Display(Name = "Special Instruction")]
        public string SpecialInstruction { get; set; }

        [Display(Name = "Requisition")]
        public string Requisition { get; set; }
        
    }

    public class AllocationFilter
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public long BookingId { get; set; }

        public int CarModelid { get; set; }

        public int DriverId { get; set; }

        public IEnumerable<DriverAllocationViewModel> AllocationList { get; set; }
    }
}