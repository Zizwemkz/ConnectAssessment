using System;
using ConnectAssessment.Service;
using NUnit.Framework;

namespace ConnectAssessment.Tests.Tests.Service
{
    [TestFixture]
    public class TransactionFeeServiceTests
    {
        private TransactionFeeService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new TransactionFeeService();
        }

        [Test]
        public void CalculateFee_ReturnsCorrectFee_ForValidAmount()
        {
            var amount = 1000m;
            var fee = _service.CalculateFee(amount);
            Assert.That(fee, Is.EqualTo(Math.Round(amount / 100 * 0.11m, 2)));
        }

        [Test]
        public void CalculateFee_ThrowsArgumentException_ForZeroOrNegativeAmount()
        {
            var ex = Assert.Throws<Exception>(() => _service.CalculateFee(0m));
            Assert.That(ex.Message, Does.Contain("An unexpected error occurred while calculating the fee."));
            Assert.That(ex.InnerException, Is.TypeOf<ArgumentException>());
            Assert.That(((ArgumentException)ex.InnerException).ParamName, Is.EqualTo("amount"));
        }
    }
}