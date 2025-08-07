using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectAssessment.Data.Models.Entities
{
    public class tbSettlementTransaction
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public decimal TransactionFee { get; set; }
        public DateTime Date { get; set; }
        public bool Success { get; set; }

        public tbCustomer Customer { get; set; }
    }
}
