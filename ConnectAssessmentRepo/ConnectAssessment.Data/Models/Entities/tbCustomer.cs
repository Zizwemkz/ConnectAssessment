using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConnectAssessment.Data.Models.Entities
{
    public class tbCustomer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Surname cannot be longer than 50 characters.")]
        public string SurName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Account Number cannot be longer than 30 characters.")]
        public string AccountNumber { get; set; }

        [StringLength(50, ErrorMessage = "Branch cannot be longer than 50 characters.")]
        public string Branch { get; set; }

        // Navigation property for transactions (optional, for convenience)
        public ICollection<tbSettlementTransaction> SettlementTransactions { get; set; }
    }
}
