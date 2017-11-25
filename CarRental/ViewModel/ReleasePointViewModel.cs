using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModel
{
    public class ReleasePointViewModel
    {
        [Key]
        public int? ReleasePointId { get; set; }

        [Required, Display(Name = "Release Point")]
        public string ReleasePointName { get; set; }
    }
}