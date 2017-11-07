using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModel
{
    public class CompanyViewModel
    {
        [Key]
        public int? CompanyId { get; set; }

        [Required, Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required, Display(Name = "Address")]
        public string Address { get; set; }

        [Required, Display(Name = "PAN Number")]
        public string PanNumber { get; set; }

        [Required, Display(Name = "Telephone 1")]
        public string Telephone1 { get; set; }

        [Display(Name = "Telephone 2")]
        public string Telephone2 { get; set; }

        [Display(Name = "Service Tax Number")]
        public string ServiceTaxNumber { get; set; }
    }
}