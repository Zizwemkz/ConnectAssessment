using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectAssessment.Common.Service
{
    public interface IBankApiService
    {
        Task<bool> TransferFundsAsync(string accountNumber, decimal amount);
    }
}
