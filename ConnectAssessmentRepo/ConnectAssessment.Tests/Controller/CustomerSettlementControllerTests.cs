using System.Threading.Tasks;
using Bogus;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using ConnectAssessment.Data.Models.Responss;
using ConnectAssessment.Data.Models.Requests;
using ConnectAssessment.Service;
using ConnectAssessment.Common.Service;

namespace ConnectAssessment.Tests.Controllers
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

        //[Test]
        //public async Task SettleCustomer_ReturnsOk_WithExpectedResponse()
        //{
        //    // Arrange
        //    var request = new Faker<SettleCustomerRequest>()
        //        .RuleFor(r => r.CustomerId, _ => _faker.Random.Int(1, 1000))
        //        .RuleFor(r => r.Amount, _ => _faker.Finance.Amount(1000, 100000))
        //        .Generate();

        //    var expectedResponse = new Faker<SettleCustomerResponse>()
        //        .RuleFor(r => r.Success, _ => true)
        //        .RuleFor(r => r.TransactionFee, _ => _faker.Finance.Amount(1, 100))
        //        .RuleFor(r => r.Message, _ => _faker.Commerce).Generate();

        //    _settlementServiceMock
        //        .Setup(s => s.SettleCustomerAsync(request))
        //        .ReturnsAsync(expectedResponse);

        //    // Act
        //    var result = await _controller.SettleCustomer(request) as OkObjectResult;
        //    var response = result?.Value as SettleCustomerResponse;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(200, result.StatusCode);
        //    Assert.IsNotNull(response);
        //    Assert.IsTrue(response.Success);
        //    Assert.AreEqual(expectedResponse.Fee, response.Fee);
        //    Assert.AreEqual(expectedResponse.AmountTransferred, response.AmountTransferred);
        //}

        //[Test]
        //public async Task SettleCustomer_ReturnsOk_WithFailureResponse()
        //{
        //    // Arrange
        //    var request = new Faker<SettleCustomerRequest>()
        //        .RuleFor(r => r.CustomerId, _ => _faker.Random.Int(1, 1000))
        //        .RuleFor(r => r.Amount, _ => _faker.Finance.Amount(1000, 100000))
        //        .Generate();

        //    var expectedResponse = new SettleCustomerResponse
        //    {
        //        Success = false,
        //        TransactionFee = 0.0m,
        //        Message = "Settlement failed due to insufficient funds."
        //    };

        //    _settlementServiceMock
        //        .Setup(s => s.SettleCustomerAsync(request))
        //        .ReturnsAsync(expectedResponse);

        //    // Act
        //    var result = await _controller.SettleCustomer(request) as OkObjectResult;
        //    var response = result?.Value as SettleCustomerResponse;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(200, result.StatusCode);
        //    Assert.IsNotNull(response);
        //    Assert.IsFalse(response.Success);
        //    Assert.AreEqual(0.0m, response.TransactionFee);
        //    Assert.AreEqual(0.0m, response.Message);
        //}
    }
}