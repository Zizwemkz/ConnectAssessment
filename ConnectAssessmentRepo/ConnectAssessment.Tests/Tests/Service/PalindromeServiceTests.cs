using System;
using ConnectAssessment.Service;
using NUnit.Framework;

namespace ConnectAssessment.Tests.Tests.Service
{
    [TestFixture]
    public class PalindromeServiceTests
    {
        private PalindromeService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new PalindromeService();
        }

        [Test]
        public void IsPalindrome_ReturnsTrue_ForPalindrome()
        {
            var input = "A man, a plan, a canal: Panama";
            var result = _service.IsPalindrome(input, out var normalized);
            Assert.That(result, Is.True);
            Assert.That(normalized, Does.Contain("is a palindrome"));
        }

        [Test]
        public void IsPalindrome_ReturnsFalse_ForNonPalindrome()
        {
            var input = "Hello, World!";
            var result = _service.IsPalindrome(input, out var normalized);
            Assert.That(result, Is.False);
            Assert.That(normalized, Does.Contain("is not a palindrome"));
        }

        [Test]
        public void IsPalindrome_ThrowsArgumentException_ForNullOrWhitespace()
        {
            var ex = Assert.Throws<Exception>(() => _service.IsPalindrome("   ", out var _));
            Assert.That(ex.Message, Does.Contain("An unexpected error occurred while checking palindrome."));
            Assert.That(ex.InnerException, Is.TypeOf<ArgumentException>());
            Assert.That(ex.InnerException.Message, Does.Contain("Input cannot be null, empty, or whitespace."));
        }
    }
}