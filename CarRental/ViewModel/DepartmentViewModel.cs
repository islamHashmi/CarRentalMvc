using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModel
{
    public class DepartmentViewModel
    {
        [Key]
        public int? DepartmentId { get; set; }

        [Required, Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
    }
}