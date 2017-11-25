using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CarRental.ViewModel
{
    public class PartyViewModel
    {
        [Key]
        public int? PartyId { get; set; }

        [Display(Name = "Company")]
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public SelectList CompanyList { get; set; }

        [Required, Display(Name = "Branch")]
        public int? BranchId { get; set; }
        public string BranchName { get; set; }
        public SelectList BranchList { get; set; }

        [Required, Display(Name = "Status")]
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public SelectList StatusList { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Bill Name")]
        public string BillName { get; set; }

        [Display(Name = "Id Name")]
        public string IdName { get; set; }

        [Display(Name = "Address")]
        public string Address1 { get; set; }

        [Display(Name = "City")]
        public string City1 { get; set; }

        [Display(Name = "Pincode")]
        [DataType(DataType.PostalCode)]
        public string Pincode1 { get; set; }

        [Display(Name = "Address")]
        public string Address2 { get; set; }

        [Display(Name = "City")]
        public string City2 { get; set; }

        [Display(Name = "Pincode")]
        [DataType(DataType.PostalCode)]
        public string Pincode2 { get; set; }

        [Display(Name = "Contact")]
        [DataType(DataType.PhoneNumber)]
        public string Contact1 { get; set; }

        [Display(Name = "Contact")]
        [DataType(DataType.PhoneNumber)]
        public string Contact2 { get; set; }

        [Display(Name = "Fax Number")]
        [DataType(DataType.PhoneNumber)]
        public string FaxNumber { get; set; }

        [Display(Name = "Email Id")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Display(Name = "Main Group")]
        public int? PrimaryGroupId { get; set; }
        public string PrimaryGroupName { get; set; }
        public SelectList PrimaryGroupList { get; set; }

        [Display(Name = "Discount Allowed")]
        public bool DiscountAllowed { get; set; }

        [Required, Display(Name = "Duty Format Slip")]
        public string DutySlipFormat { get; set; }

        [Display(Name = "Vendor Code")]
        public string VendorCode { get; set; }

        [Display(Name = "Cost Center")]
        public string CostCenter { get; set; }

        [Display(Name = "TDs Percent")]
        public decimal? TdsPercent { get; set; }

        [Display(Name = "Commisssion Percent")]
        public decimal? CommissionPercent { get; set; }

        [Display(Name = "GST Number")]
        public string GstNumber { get; set; }
    }
}