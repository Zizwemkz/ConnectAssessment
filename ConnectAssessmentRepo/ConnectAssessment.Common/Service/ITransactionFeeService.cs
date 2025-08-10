using System;

namespace ConnectAssessment.Common.Service
{
    public interface ITransactionFeeService
    {
        decimal CalculateFee(decimal amount);
    }
}
