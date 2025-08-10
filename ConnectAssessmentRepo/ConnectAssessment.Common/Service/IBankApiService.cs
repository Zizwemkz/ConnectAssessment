using System;

namespace ConnectAssessment.Common.Service
{
    public interface IBankApiService
    {
        Task<bool> TransferFundsAsync(string accountNumber, decimal amount);
    }
}
