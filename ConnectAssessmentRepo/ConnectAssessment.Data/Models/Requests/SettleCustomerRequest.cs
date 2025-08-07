using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectAssessment.Data.Models.Requests
{
    public class SettleCustomerRequest
    {
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
    }
}
