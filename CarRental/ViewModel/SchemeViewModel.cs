using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModel
{
    public class SchemeViewModel
    {
        [Key]
        public int? SchemeId { get; set; }

        [Required, Display(Name = "Scheme Name")]
        public string SchemeName { get; set; }

        [Required, Display(Name = "Min. Hours")]
        public decimal? MinimumHours { get; set; }

        [Required, Display(Name = "Min. Kilometer")]
        public decimal? MinimumKilometer { get; set; }
    }
}