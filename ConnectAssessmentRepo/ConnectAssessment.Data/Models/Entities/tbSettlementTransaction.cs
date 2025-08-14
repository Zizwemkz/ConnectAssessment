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
        [ForeignKey("tbCustomer")]
        public int CustomerId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal TransactionFee { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool Success { get; set; }

        public tbCustomer Customer { get; set; }
    }
}
