using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConnectAssessment.Common.Repository;
using ConnectAssessment.Data;
using ConnectAssessment.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConnectAssessment.Repositories
{
    public class SettlementRepository : ISettlementRepository
    {
        private readonly DbContextOptions<ConnectAssessmentDbContext> _options;
        public SettlementRepository(DbContextOptions<ConnectAssessmentDbContext> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options), "DbContextOptions cannot be null.");
        }

        public async Task<tbSettlementTransaction> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Transaction ID must be a positive integer.", nameof(id));
            try
            {
                using (var context = new ConnectAssessmentDbContext(_options))
                {
                    var result = await context.tbSettlementTransactions.FindAsync(id);
                    if (result == null)
                        throw new KeyNotFoundException($"Settlement transaction with ID {id} not found.");
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in GetByIdAsync: {ex.Message}");
                throw new Exception("An unexpected error occurred while retrieving the transaction.", ex);
            }
        }

        public async Task<IEnumerable<tbSettlementTransaction>> GetAllAsync()
        {
            try
            {
                using (var context = new ConnectAssessmentDbContext(_options))
                {
                    return await context.tbSettlementTransactions.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in GetAllAsync: {ex.Message}");
                throw new Exception("An unexpected error occurred while retrieving all settlement transactions.", ex);
            }
        }

        public async Task AddAsync(tbSettlementTransaction settlementTrans)
        {
            ValidateSettlementTransaction(settlementTrans);

            try
            {
                using (var context = new ConnectAssessmentDbContext(_options))
                {
                    await context.tbSettlementTransactions.AddAsync(settlementTrans);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in AddAsync: {ex.Message}");
                throw new Exception("An unexpected error occurred while adding the settlement transaction.", ex);
            }
        }

        public async Task UpdateAsync(tbSettlementTransaction settlementTrans)
        {
            ValidateSettlementTransaction(settlementTrans);

            try
            {
                using (var context = new ConnectAssessmentDbContext(_options))
                {
                    context.tbSettlementTransactions.Update(settlementTrans);
                    await context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database update error in UpdateAsync: {dbEx.Message}");
                throw new Exception("A database error occurred while updating the settlement transaction.", dbEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in UpdateAsync: {ex.Message}");
                throw new Exception("An unexpected error occurred while updating the settlement transaction.", ex);
            }
        }

        private void ValidateSettlementTransaction(tbSettlementTransaction trans)
        {
            if (trans == null)
                throw new ArgumentNullException(nameof(trans), "Settlement transaction cannot be null.");

            if (trans.Amount <= 0)
                throw new ArgumentException("Settlement amount must be greater than zero.", nameof(trans.Amount));
            if (trans.CustomerId <= 0)
                throw new ArgumentException("CustomerId must be greater than zero.", nameof(trans.Amount));
            if (trans.Date == default)
                throw new ArgumentException("Date is required.", nameof(trans.Date));
            if (trans.TransactionFee <= 0)
                throw new ArgumentException("Transaction fee is required.", nameof(trans.TransactionFee));
        }
    }
}