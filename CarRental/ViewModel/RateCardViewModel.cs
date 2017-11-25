using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CarRental.ViewModel
{
    public class RateCardViewModel
    {
        [Key]
        public int? RateId { get; set; }

        [Required, Display(Name = "Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EfftectiveDate { get; set; }

        [Display(Name ="Company")]
        public int? PartyId { get; set; }
        public string PartyName { get; set; }
        public SelectList PartyList { get; set; }

        [Display(Name = "Model")]
        public int? CarModelId { get; set; }
        public string CarModelName { get; set; }
        public SelectList CarModelList { get; set; }

        [Display(Name = "Duty Type")]
        public int? DutyTypeId { get; set; }
        public string DutyTypeName { get; set; }
        public SelectList DutyTypeList { get; set; }

        [Display(Name = "Scheme")]
        public int? SchemeId { get; set; }
        public string SchemeName { get; set; }
        public SelectList SchemeList { get; set; }

        [Display(Name = "Min. Hours")]
        public decimal? MinHours { get; set; }

        [Display(Name = "Min. K.M.")]
        public decimal? MinKm { get; set; }

        [Display(Name = "Rate")]
        public decimal? RateAmount { get; set; }

        [Display(Name = "Extra Hrs.")]
        public decimal? ExtraHours { get; set; }

        [Display(Name = "Extra K.M.")]
        public decimal? ExtraKM { get; set; }

        [Display(Name = "Night Allow")]
        public decimal? NightAllowance { get; set; }

        [Display(Name = "Day Allow")]
        public decimal? DayAllowance { get; set; }
    }
}