using NUnit.Framework;
//using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ConnectAssessment.Common.Service;
using ConnectAssessment.Data.Models.Requests;
using ConnectAssessment.Data.Models.Responss;

namespace ConnectAssessment.Tests.Tests.Controller
{
    [TestFixture]
    public class PalindromeControllerTests
    {
        private Mock<IPalindromeService> _palindromeServiceMock;
        private PalindromeController _controller;

        [SetUp]
        public void SetUp()
        {
            _palindromeServiceMock = new Mock<IPalindromeService>();
            _controller = new PalindromeController(_palindromeServiceMock.Object);
        }

        [Test]
        public void CheckPalindrome_ReturnsOkResult_WithExpectedResponse_WhenPalindrome()
        {
            // Arrange
            var request = new PalindromeRequest { statement = "A man, a plan, a canal: Panama" };
            var expectedNormalized = "amanaplanacanalpanama : is a palindrome";
            string outValue;

            _palindromeServiceMock
                 .Setup(s => s.IsPalindrome(request.statement, out It.Ref<string>.IsAny))
                 .Returns((string input, out string normalized) => { normalized = expectedNormalized; return true; });

            // Act
            var actionResult = _controller.CheckPalindrome(request);
            var result = actionResult.Result as OkObjectResult;
            var response = result?.Value as PalindromeResponse;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(200, Is.EqualTo(result.StatusCode));
            Assert.That(response, Is.Not.Null);
            Assert.That(response.IsPalindrome, Is.True);
            Assert.That(expectedNormalized, Is.EqualTo(response.Normalized));
        }

        [Test]
        public void CheckPalindrome_ReturnsOkResult_WithExpectedResponse_WhenNotPalindrome()
        {
            // Arrange
            var request = new PalindromeRequest { statement = "Hello World" };
            var expectedNormalized = "helloworld";

            _palindromeServiceMock
                .Setup(s => s.IsPalindrome(request.statement, out It.Ref<string>.IsAny))
                .Returns((string input, out string normalized) => { normalized = expectedNormalized; return false; });

            // Act
            var actionResult = _controller.CheckPalindrome(request);
            var result = actionResult.Result as OkObjectResult;
            var response = result?.Value as PalindromeResponse;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(200, Is.EqualTo(result.StatusCode));
            Assert.That(response, Is.Not.Null);
            Assert.That(response.IsPalindrome, Is.False);
            Assert.That(expectedNormalized, Is.EqualTo(response.Normalized));
        }
    }
}