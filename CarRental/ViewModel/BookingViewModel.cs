using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CarRental.ViewModel
{
    public class BookingViewModel
    {
        [Key]
        public long? BookingId { get; set; }

        [Required, Display(Name = "Branch")]
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public SelectList BranchList { get; set; }

        [Display(Name = "Booking Type")]
        public string BookingType { get; set; }
        public string BookingTypeDescription { get; set; }

        [Display(Name = "Billing Address")]
        public string BillAddress { get; set; }

        [Display(Name = "Booking No.")]
        public string BookingNumber { get; set; }

        [Required,Display(Name = "Booking Date")]
        public DateTime? BookingDate { get; set; }

        [Display(Name = "Billing Type")]
        public string BillingTypeCode { get; set; }
        public string BiilingType { get; set; }

        [Display(Name = "Booking Received By")]
        public string BookingReceivedByName { get; set; }
        public int BookingReceivedBy { get; set; }        

        [Display(Name = "Company")]
        public int? PartyId { get; set; }
        public string PartyName { get; set; }
        public SelectList PartyList { get; set; }

        [Display(Name = "Booking By")]
        public string BookingBy { get; set; }

        [Display(Name = "Mobile No. (Book By)")]
        public string BookedByMobileNo { get; set; }

        [Display(Name = "Guest")]
        public int? GuestId { get; set; }
        public string GuestName { get; set; }
        public SelectList GuestList { get; set; }
        
        [Display(Name = "Booking Source")]
        public string BookingSource { get; set; }

        [Display(Name = "Requirement Date From")]
        public DateTime? RequirementStartDate { get; set; }

        [Display(Name = "Requirement Date To")]
        public DateTime? RequirementEndDate { get; set; }

        [Display(Name = "Travel Id")]
        public string TravelId { get; set; }

        [Display(Name = "Reporting To")]
        public string ReportingTo { get; set; }

        [Display(Name = "Mobile No.")]
        public string ReportingMobileNo { get; set; }

        [Display(Name = "Reporting Location")]
        public string ReportingLocation { get; set; }
        public int? GuestAddressId { get; set; }

        [Display(Name = "Reporting Time")]
        public TimeSpan? ReportingTime { get; set; }

        [Display(Name = "Model")]
        public int? CarModelId { get; set; }
        public string CarModelName { get; set; }
        public SelectList CarModelList { get; set; }

        [Display(Name = "Duty Type")]
        public int? DutyTypeId { get; set; }
        public string DutyTypeName { get; set; }
        public SelectList DutyTypeList { get; set; }

        [Display(Name = "Special Instruction")]
        public string SpecialInstruction { get; set; }

        [Display(Name = "Requisition")]
        public bool? Requisition { get; set; }

        [Display(Name = "Mobile 1")]
        public string MobileNumber1 { get; set; }

        [Display(Name = "Mobile 2")]
        public string MobileNumber2 { get; set; }

        [Display(Name = "Mobile 3")]
        public string MobileNumber3 { get; set; }
    }

    public class BookingFilter
    {
        public string BookingType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime Enddate { get; set; }

        public IEnumerable<BookingViewModel> BookingList { get; set; }
    }
}