using System;
using System.Collections.Generic;

namespace ConnectAssessment.Data.Models.Responss
{
    public class SettleCustomerResponse
    {
        public bool Success { get; set; }
        public decimal TransactionFee { get; set; }
        public string Message { get; set; }
    }
}
