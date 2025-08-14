using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConnectAssessment.Data.Models.Entities
{
    public class tbCustomer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string SurName { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        public string Branch { get; set; }

        public ICollection<tbSettlementTransaction> SettlementTransactions { get; set; }
    }
}
