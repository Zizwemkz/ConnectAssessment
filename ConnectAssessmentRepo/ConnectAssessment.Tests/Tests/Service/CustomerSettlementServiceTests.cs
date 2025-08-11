using System;
using System.Threading.Tasks;
using ConnectAssessment.Common.Repository;
using ConnectAssessment.Common.Service;
using ConnectAssessment.Data.Models.Entities;
using ConnectAssessment.Data.Models.Requests;
using ConnectAssessment.Data.Models.Responss;
using Moq;
using NUnit.Framework;

namespace ConnectAssessment.Tests.Tests.Service
{
    [TestFixture]
    public class CustomerSettlementServiceTests
    {
        private Mock<ICustomerRepository> _customerRepoMock;
        private Mock<ISettlementRepository> _settlementRepoMock;
        private Mock<IBankApiService> _bankApiMock;
        private Mock<ITransactionFeeService> _feeServiceMock;
        private CustomerSettlementService _service;

        [SetUp]
        public void SetUp()
        {
            _customerRepoMock = new Mock<ICustomerRepository>();
            _settlementRepoMock = new Mock<ISettlementRepository>();
            _bankApiMock = new Mock<IBankApiService>();
            _feeServiceMock = new Mock<ITransactionFeeService>();
            _service = new CustomerSettlementService(
                _customerRepoMock.Object,
                _settlementRepoMock.Object,
                _bankApiMock.Object,
                _feeServiceMock.Object
            );
        }

        [Test]
        public void SettleCustomerAsync_Throws_WhenRequestIsNull()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _service.SettleCustomerAsync(null));
        }

        [Test]
        public async Task SettleCustomerAsync_ReturnsFailure_WhenCustomerIdIsInvalid()
        {
            var req = new SettleCustomerRequest { CustomerId = 0, Amount = 100m };
            var resp = await _service.SettleCustomerAsync(req);
            Assert.That(resp.Success, Is.False);
            Assert.That(resp.Message, Is.EqualTo("Invalid customer ID."));
        }

        [Test]
        public async Task SettleCustomerAsync_ReturnsFailure_WhenAmountIsInvalid()
        {
            var req = new SettleCustomerRequest { CustomerId = 123, Amount = 0m };
            var resp = await _service.SettleCustomerAsync(req);
            Assert.That(resp.Success, Is.False);
            Assert.That(resp.Message, Is.EqualTo("Amount must be greater than zero."));
        }

        [Test]
        public async Task SettleCustomerAsync_ReturnsFailure_WhenCustomerDoesNotExist()
        {
            var req = new SettleCustomerRequest { CustomerId = 123, Amount = 100m };
            _customerRepoMock.Setup(r => r.GetByIdAsync(req.CustomerId)).ReturnsAsync((tbCustomer)null);

            var resp = await _service.SettleCustomerAsync(req);
            Assert.That(resp.Success, Is.False);
            Assert.That(resp.Message, Is.EqualTo("tbCustomer not found"));
        }

        [Test]
        public async Task SettleCustomerAsync_ReturnsFailure_WhenBankApiFails()
        {
            var req = new SettleCustomerRequest { CustomerId = 1, Amount = 100m };
            var customer = new tbCustomer { Id = 1, AccountNumber = "ACC123" };

            _customerRepoMock.Setup(r => r.GetByIdAsync(req.CustomerId)).ReturnsAsync(customer);
            _feeServiceMock.Setup(f => f.CalculateFee(100m)).Returns(1.11m);
            _bankApiMock.Setup(b => b.TransferFundsAsync("ACC123", 98.89m)).ReturnsAsync(false);

            _settlementRepoMock.Setup(s => s.AddAsync(It.IsAny<tbSettlementTransaction>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var resp = await _service.SettleCustomerAsync(req);
            Assert.That(resp.Success, Is.False);
            Assert.That(resp.Message, Is.EqualTo("Failed to settle"));
        }

        [Test]
        public async Task SettleCustomerAsync_ReturnsSuccess_WhenAllStepsPass()
        {
            var req = new SettleCustomerRequest { CustomerId = 1, Amount = 100m };
            var customer = new tbCustomer { Id = 1, AccountNumber = "ACC123" };

            _customerRepoMock.Setup(r => r.GetByIdAsync(req.CustomerId)).ReturnsAsync(customer);
            _feeServiceMock.Setup(f => f.CalculateFee(100m)).Returns(1.11m);
            _bankApiMock.Setup(b => b.TransferFundsAsync("ACC123", 98.89m)).ReturnsAsync(true);

            _settlementRepoMock.Setup(s => s.AddAsync(It.IsAny<tbSettlementTransaction>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var resp = await _service.SettleCustomerAsync(req);
            Assert.That(resp.Success, Is.True);
            Assert.That(resp.Message, Is.EqualTo("Settled successfully"));
            Assert.That(resp.TransactionFee, Is.EqualTo(1.11m));
        }

        [Test]
        public async Task SettleCustomerAsync_ReturnsFailure_WhenExceptionThrown()
        {
            var req = new SettleCustomerRequest { CustomerId = 1, Amount = 100m };
            _customerRepoMock.Setup(r => r.GetByIdAsync(req.CustomerId)).ThrowsAsync(new Exception("DB error"));

            var resp = await _service.SettleCustomerAsync(req);
            Assert.That(resp.Success, Is.False);
            Assert.That(resp.Message, Does.Contain("An error occurred while settling: DB error"));
        }
    }
}