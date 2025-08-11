using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ConnectAssessment.Common.Service;
using ConnectAssessment.Data.Models.Responss;
using ConnectAssessment.Data.Models.Requests;
using Bogus;

namespace ConnectAssessment.Tests.Tests.Controller
{
    [TestFixture]
    public class CustomerSettlementControllerTests
    {
        private Mock<ICustomerSettlementService> _settlementServiceMock;
        private CustomerSettlementController _controller;
        private Faker _faker;

        [SetUp]
        public void SetUp()
        {
            _settlementServiceMock = new Mock<ICustomerSettlementService>();
            _controller = new CustomerSettlementController(_settlementServiceMock.Object);
            _faker = new Faker();
        }

        [Test]
        public async Task SettleCustomer_ReturnsOk_WithSuccessfullResponse()
        {
            // Arrange
            var request = new Faker<SettleCustomerRequest>()
                .RuleFor(r => r.CustomerId, _ => _faker.Random.Int(1, 1000))
                .RuleFor(r => r.Amount, _ => _faker.Finance.Amount(1000, 100000))
                .Generate();

            var expectedResponse = new Faker<SettleCustomerResponse>()
                .RuleFor(r => r.Success, _ => true)
                .RuleFor(r => r.TransactionFee, _ => _faker.Finance.Amount(1, 100))
                .RuleFor(r => r.Message, _ => _faker.Lorem.Sentence())
                .Generate();

            _settlementServiceMock
                .Setup(s => s.SettleCustomerAsync(request))
                .ReturnsAsync(expectedResponse);

            // Act
            var actionResult = await _controller.SettleCustomer(request);
            var result = actionResult.Result as OkObjectResult;
            var response = result?.Value as SettleCustomerResponse;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.True);
            Assert.That(response.TransactionFee, Is.EqualTo(expectedResponse.TransactionFee));
            Assert.That(response.Message, Is.EqualTo(expectedResponse.Message));
        }

        [Test]
        public async Task SettleCustomer_ReturnsOk_WithFailureResponse()
        {
            // Arrange
            var request = new Faker<SettleCustomerRequest>()
                .RuleFor(r => r.CustomerId, _ => _faker.Random.Int(1, 1000))
                .RuleFor(r => r.Amount, _ => _faker.Finance.Amount(1000, 100000))
                .Generate();

            var expectedResponse = new SettleCustomerResponse
            {
                Success = false,
                TransactionFee = 0.0m,
                Message = "Settlement failed due to insufficient funds."
            };

            _settlementServiceMock
                .Setup(s => s.SettleCustomerAsync(request))
                .ReturnsAsync(expectedResponse);

            // Act
            var actionResult = await _controller.SettleCustomer(request);
            var result = actionResult.Result as OkObjectResult;
            var response = result?.Value as SettleCustomerResponse;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Success, Is.False);
            Assert.That(response.TransactionFee, Is.EqualTo(0.0m));
            Assert.That(response.Message, Is.EqualTo("Settlement failed due to insufficient funds."));
        }
    }
}