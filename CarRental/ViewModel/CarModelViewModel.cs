using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModel
{
    public class CarModelViewModel
    {
        [Key]
        public int? CarModelId { get; set; }

        [Required, Display(Name = "Model Name")]
        public string ModelDescription { get; set; }

        [Display(Name ="Seating Capacity")]
        public int? SeatingCapacity { get; set; }
    }
}