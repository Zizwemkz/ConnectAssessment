using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ConnectAssessment.Common.Service;
using ConnectAssessment.Data.Models.Requests;
using ConnectAssessment.Data.Models.Responss;

namespace ConnectAssessment.Tests.Controllers
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

        //[Test]
        //public void CheckPalindrome_ReturnsOkResult_WithExpectedResponse_WhenPalindrome()
        //{
        //    // Arrange
        //    var request = new PalindromeRequest { statement = "A man, a plan, a canal: Panama" };
        //    var expectedNormalized = "amanaplanacanalpanama";
        //    _palindromeServiceMock
        //        .Setup(s => s.IsPalindrome(request.statement, out expectedNormalized))
        //        .Returns(true);

        //    // Act
        //    var result = _controller.CheckPalindrome(request) as OkObjectResult;
        //    var response = result?.Value as PalindromeResponse;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(200, result.StatusCode);
        //    Assert.IsNotNull(response);
        //    Assert.IsTrue(response.IsPalindrome);
        //    Assert.AreEqual(expectedNormalized, response.Normalized);
        //}

        //[Test]
        //public void CheckPalindrome_ReturnsOkResult_WithExpectedResponse_WhenNotPalindrome()
        //{
        //    // Arrange
        //    var request = new PalindromeRequest { statement = "Hello World" };
        //    var expectedNormalized = "helloworld";
        //    _palindromeServiceMock
        //        .Setup(s => s.IsPalindrome(request.statement, out expectedNormalized))
        //        .Returns(false);

        //    // Act
        //    var result = _controller.CheckPalindrome(request) as OkObjectResult;
        //    var response = result?.Value as PalindromeResponse;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(200, result.StatusCode);
        //    Assert.IsNotNull(response);
        //    Assert.IsFalse(response.IsPalindrome);
        //    Assert.AreEqual(expectedNormalized, response.Normalized);
        //}
    }
}