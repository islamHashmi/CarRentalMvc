using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CarRental.ViewModel
{
    public class GuestViewModel
    {
        [Key]
        public int? GuestId { get; set; }

        [Required, Display(Name = "Branch")]
        public int? BranchId { get; set; }
        public string BranchName { get; set; }
        public SelectList BranchList { get; set; }

        [Required, Display(Name = "Party")]
        public int? PartyId { get; set; }
        public string PartyName { get; set; }
        public SelectList PartyList { get; set; }

        [Display(Name = "Guest Name")]
        public string GuestName { get; set; }

        [Display(Name ="Contact No. (Guest)")]
        public string GuestMobile { get; set; }

        [Display(Name = "Booked By")]
        public string BookedBy { get; set; }

        [Display(Name = "Contact No. (Booked By)")]
        public string BookedByMobile { get; set; }

        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public SelectList DepartmentList { get; set; }

        [Required, Display(Name = "Model Name")]
        public int? CarModelId { get; set; }
        public string ModelName { get; set; }
        public SelectList ModelList { get; set; }

        [Required, Display(Name = "Contact No. 1")]
        public string ContactNumber1 { get; set; }

        [Display(Name = "Contact No. 2")]
        public string ContactNumber2 { get; set; }

        [Display(Name = "Contact No. 3")]
        public string ContactNumber3 { get; set; }
    }
}