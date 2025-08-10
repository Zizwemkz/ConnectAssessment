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
