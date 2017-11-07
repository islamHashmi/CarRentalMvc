using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModel
{
    public class DesignationViewModel
    {
        [Key]
        public int? DesignationId { get; set; }

        [Required,Display(Name ="Designation Name")]
        public string DesignationName { get; set; }
    }
}