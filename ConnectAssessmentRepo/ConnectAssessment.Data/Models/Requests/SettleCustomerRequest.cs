using System;
using System.Collections.Generic;

namespace ConnectAssessment.Data.Models.Requests
{
    public class SettleCustomerRequest
    {
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
    }
}
