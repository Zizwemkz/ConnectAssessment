using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectAssessment.Data.Models.Requests;
using ConnectAssessment.Data.Models.Responss;

namespace ConnectAssessment.Common.Service
{
    public interface ICustomerSettlementService
    {
        Task<SettleCustomerResponse> SettleCustomerAsync(SettleCustomerRequest request);
    }
}
