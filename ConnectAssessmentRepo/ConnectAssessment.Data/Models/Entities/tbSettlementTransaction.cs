using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConnectAssessment.Data.Models.Entities
{
    public class tbSettlementTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Transaction Fee cannot be negative.")]
        public decimal TransactionFee { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool Success { get; set; }

        // Navigation property
        public tbCustomer Customer { get; set; }
    }
}
