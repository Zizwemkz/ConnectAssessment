using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ConnectAssessment.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace ConnectAssessment.Tests.Tests.Service
{
    [TestFixture]
    public class BankApiServiceTests : IDisposable
    {
        private Mock<IConfiguration> _configMock;
        private HttpClient? _httpClient;
        private Mock<HttpMessageHandler> _handlerMock;

        [SetUp]
        public void SetUp()
        {
            _configMock = new Mock<IConfiguration>();
            _handlerMock = new Mock<HttpMessageHandler>();
        }

        [Test]
        public async Task TransferFundsAsync_ReturnsTrue_WhenSuccessStatusCode()
        {
            // Arrange
            var baseUrl = "https://fakebank.com/api/transfer";
            _configMock.Setup(c => c["BankApi:BaseUrl"]).Returns(baseUrl);

            _handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

            _httpClient = new HttpClient(_handlerMock.Object);
            var service = new BankApiService(_httpClient, _configMock.Object);

            // Act
            var result = await service.TransferFundsAsync("123456789", 100m);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task TransferFundsAsync_ReturnsFalse_WhenNonSuccessStatusCode()
        {
            // Arrange
            var baseUrl = "https://fakebank.com/api/transfer";
            _configMock.Setup(c => c["BankApi:BaseUrl"]).Returns(baseUrl);

            _handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest });

            _httpClient = new HttpClient(_handlerMock.Object);
            var service = new BankApiService(_httpClient, _configMock.Object);

            // Act
            var result = await service.TransferFundsAsync("123456789", 100m);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void TransferFundsAsync_Throws_WhenAccountNumberIsEmpty()
        {
            var service = new BankApiService(new HttpClient(), _configMock.Object);
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await service.TransferFundsAsync("", 100m));
            Assert.That(ex.ParamName, Is.EqualTo("accountNumber"));
        }

        [Test]
        public void TransferFundsAsync_Throws_WhenAmountIsZeroOrNegative()
        {
            var service = new BankApiService(new HttpClient(), _configMock.Object);
            var ex = Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                await service.TransferFundsAsync("123456789", 0m));
            Assert.That(ex.ParamName, Is.EqualTo("amount"));
        }

        [Test]
        public void TransferFundsAsync_Throws_WhenBankApiBaseUrlIsMissing()
        {
            _configMock.Setup(c => c["BankApi:BaseUrl"]).Returns("");
            var service = new BankApiService(new HttpClient(), _configMock.Object);
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.TransferFundsAsync("123456789", 100m));
            Assert.That(ex.Message, Does.Contain("Bank API base URL is not configured"));
        }

        [Test]
        public async Task TransferFundsAsync_ReturnsFalse_OnHttpRequestException()
        {
            var baseUrl = "https://fakebank.com/api/transfer";
            _configMock.Setup(c => c["BankApi:BaseUrl"]).Returns(baseUrl);

            _handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("Network error"));

            _httpClient = new HttpClient(_handlerMock.Object);
            var service = new BankApiService(_httpClient, _configMock.Object);

            var result = await service.TransferFundsAsync("123456789", 100m);
            Assert.That(result, Is.False);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}