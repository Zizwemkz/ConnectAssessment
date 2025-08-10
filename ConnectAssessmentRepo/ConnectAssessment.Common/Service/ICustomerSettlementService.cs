using System;
using ConnectAssessment.Data.Models.Requests;
using ConnectAssessment.Data.Models.Responss;

namespace ConnectAssessment.Common.Service
{
    public interface ICustomerSettlementService
    {
        Task<SettleCustomerResponse> SettleCustomerAsync(SettleCustomerRequest request);
    }
}
