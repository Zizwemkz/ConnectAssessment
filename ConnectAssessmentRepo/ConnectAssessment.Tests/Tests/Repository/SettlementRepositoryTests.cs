using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConnectAssessment.Data;
using ConnectAssessment.Data.Models.Entities;
using ConnectAssessment.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace ConnectAssessment.Tests.Repositories
{
    [TestFixture]
    public class SettlementRepositoryTests : IDisposable
    {
        private ConnectAssessmentDbContext _context;
        private SettlementRepository _repo;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ConnectAssessmentDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ConnectAssessmentDbContext(options);
            _repo = new SettlementRepository(options);
        }

        [Test]
        public async Task AddAsync_AddsSettlement_WhenValid()
        {
            var settlement = new tbSettlementTransaction
            {
                CustomerId = 1,
                Amount = 100,
                TransactionFee = 1.1m,
                Date = DateTime.UtcNow,
                Success = true
            };

            await _repo.AddAsync(settlement);
            var dbSettlement = await _context.tbSettlementTransactions.FindAsync(settlement.Id);

            Assert.That(dbSettlement, Is.Not.Null);
            Assert.That(dbSettlement.Amount, Is.EqualTo(100));
        }

        [Test]
        public void AddAsync_ThrowsArgumentNullException_WhenNull()
        {
            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () => await _repo.AddAsync(null));
            Assert.That(ex.ParamName, Is.EqualTo("trans"));
        }

        [Test]
        public void AddAsync_ThrowsArgumentException_WhenAmountInvalid()
        {
            var settlement = new tbSettlementTransaction
            {
                CustomerId = 1,
                Amount = 0,
                TransactionFee = 1,
                Date = DateTime.UtcNow,
                Success = true
            };
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _repo.AddAsync(settlement));
            Assert.That(ex.ParamName, Is.EqualTo("Amount"));
        }

        [Test]
        public void AddAsync_ThrowsArgumentException_WhenCustomerIdInvalid()
        {
            var settlement = new tbSettlementTransaction
            {
                CustomerId = 0,
                Amount = 100,
                TransactionFee = 1,
                Date = DateTime.UtcNow,
                Success = true
            };
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _repo.AddAsync(settlement));
            Assert.That(ex.ParamName, Is.EqualTo("Amount"));
        }

        [Test]
        public void AddAsync_ThrowsArgumentException_WhenDateDefault()
        {
            var settlement = new tbSettlementTransaction
            {
                CustomerId = 1,
                Amount = 100,
                TransactionFee = 1,
                Date = default,
                Success = true
            };
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _repo.AddAsync(settlement));
            Assert.That(ex.ParamName, Is.EqualTo("Date"));
        }

        [Test]
        public void AddAsync_ThrowsArgumentException_WhenFeeZero()
        {
            var settlement = new tbSettlementTransaction
            {
                CustomerId = 1,
                Amount = 100,
                TransactionFee = 0,
                Date = DateTime.UtcNow,
                Success = true
            };
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _repo.AddAsync(settlement));
            Assert.That(ex.ParamName, Is.EqualTo("TransactionFee"));
        }

        [Test]
        public async Task GetByIdAsync_ReturnsSettlement_WhenExists()
        {
            var settlement = new tbSettlementTransaction
            {
                CustomerId = 1,
                Amount = 100,
                TransactionFee = 1.1m,
                Date = DateTime.UtcNow,
                Success = true
            };
            await _context.tbSettlementTransactions.AddAsync(settlement);
            await _context.SaveChangesAsync();

            var result = await _repo.GetByIdAsync(settlement.Id);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Amount, Is.EqualTo(100));
        }

        [Test]
        public void GetByIdAsync_ThrowsArgumentException_WhenIdInvalid()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _repo.GetByIdAsync(0));
            Assert.That(ex.ParamName, Is.EqualTo("id"));
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllSettlements()
        {
            var s1 = new tbSettlementTransaction
            {
                CustomerId = 1,
                Amount = 100,
                TransactionFee = 1,
                Date = DateTime.UtcNow,
                Success = true
            };
            var s2 = new tbSettlementTransaction
            {
                CustomerId = 2,
                Amount = 200,
                TransactionFee = 2,
                Date = DateTime.UtcNow,
                Success = true
            };
            await _context.tbSettlementTransactions.AddRangeAsync(s1, s2);
            await _context.SaveChangesAsync();

            var all = await _repo.GetAllAsync();
            Assert.That(all, Has.Exactly(2).Items);
        }

        [Test]
        public async Task UpdateAsync_UpdatesSettlement_WhenValid()
        {
            var settlement = new tbSettlementTransaction
            {
                CustomerId = 1,
                Amount = 100,
                TransactionFee = 1,
                Date = DateTime.UtcNow,
                Success = true
            };
            await _context.tbSettlementTransactions.AddAsync(settlement);
            await _context.SaveChangesAsync();

            settlement.Amount = 555;
            await _repo.UpdateAsync(settlement);

            var updated = await _context.tbSettlementTransactions.FindAsync(settlement.Id);
            Assert.That(updated.Amount, Is.EqualTo(555));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}