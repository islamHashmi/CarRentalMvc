using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CarRental.ViewModel
{
    public class BranchViewModel
    {
        [Key]
        public int? BranchId { get; set; }

        [Required, Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Required, Display(Name = "Compnay Name")]
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public SelectList CompanyList { get; set; }
    }
}