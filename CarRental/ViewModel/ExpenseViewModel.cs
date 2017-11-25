using System.ComponentModel.DataAnnotations;

namespace CarRental.ViewModel
{
    public class ExpenseViewModel
    {
        [Key]
        public int? ExpenseId { get; set; }

        [Required, Display(Name = "Expense Head")]
        public string ExpenseName { get; set; }
    }
}