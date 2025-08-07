using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectAssessment.Data.Models.Entities;

namespace ConnectAssessment.Common.Repository
{
    public interface ISettlementRepository
    {
        Task<IEnumerable<tbSettlementTransaction>> GetAllAsync();
        Task AddAsync(tbSettlementTransaction settlementTrans);
        Task UpdateAsync(tbSettlementTransaction settlementTrans);
    }
}
