using System;
using Discount.Configuration;
using Discount.Domain;
using Discount.Domain.Interfaces;
using Discount.Domain.Objects;
using Moq;
using Xunit;

namespace Discount.Tests
{
    public class TransactionReaderTests
    {
        private readonly Mock<ITransactionParser> _transactionParserMock;
        private readonly ITransactionReader _transactionReader;

        public TransactionReaderTests()
        {
            _transactionParserMock = new Mock<ITransactionParser>();

            var transaction = new Transaction
            {
                Size = Constants.Sizes.Medium,
                Date = DateTime.UtcNow,
                ShippingPrice = Constants.ShippingPrices.Medium.LaPoste,
                ShippingProvider = Constants.Providers.LaPoste,
                Discount = new decimal(0.5)
            };
            
            _transactionParserMock.Setup(p => p.ParseTransaction(It.IsAny<string>()))
                .Returns(transaction);

            _transactionReader = new TransactionReader(_transactionParserMock.Object);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ShouldReadTransactions()
        {
            var response = _transactionReader.ReadTransactions();

            _transactionParserMock.Verify(p => p.ParseTransaction(It.IsAny<string>()), Times.AtLeastOnce);
            Assert.True(response.Count > 0);
        }
    }
}
