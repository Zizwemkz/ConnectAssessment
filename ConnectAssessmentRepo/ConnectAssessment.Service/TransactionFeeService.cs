using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectAssessment.Common.Service;

namespace ConnectAssessment.Service
{
    public class TransactionFeeService : ITransactionFeeService
    {
        public decimal CalculateFee(decimal amount)
        {
            try
            {
                if (amount <= 0)
                    throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

                decimal fee = Math.Round((amount / 100) * 0.11m, 2);
                return fee;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while calculating the fee.", ex);
            }
        }
    }
}
