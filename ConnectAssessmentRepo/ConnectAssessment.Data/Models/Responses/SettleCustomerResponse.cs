using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectAssessment.Data.Models.Responss
{
    public class SettleCustomerResponse
    {
        public bool Success { get; set; }
        public decimal TransactionFee { get; set; }
        public string Message { get; set; }
    }
}
