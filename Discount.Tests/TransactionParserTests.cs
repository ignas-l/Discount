using Discount.Configuration;
using Discount.Domain;
using Discount.Domain.Interfaces;
using Xunit;

namespace Discount.Tests
{
    public class TransactionParserTests
    {
        private readonly ITransactionParser _transactionParser;

        public TransactionParserTests()
        {
            _transactionParser = new TransactionParser();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ShouldParseLine()
        {
            var transaction = _transactionParser.ParseTransaction("2015-02-01 S MR ");

            Assert.Null(transaction.CorruptedData);
            Assert.Equal(Constants.Sizes.Small, transaction.Size);
            Assert.Equal(Constants.Providers.MondialRelay, transaction.ShippingProvider);
            Assert.NotNull(transaction.Date);
            Assert.Equal(2, transaction.Date.Value.Month);
            Assert.Equal(1, transaction.Date.Value.Day);
            Assert.Equal(2015, transaction.Date.Value.Year);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ShouldParseLine_CleanWhiteSpace()
        {
            var transaction = _transactionParser.ParseTransaction("     2015-02-01  S        MR      ");

            Assert.Null(transaction.CorruptedData);
            Assert.Equal(Constants.Sizes.Small, transaction.Size);
            Assert.Equal(Constants.Providers.MondialRelay, transaction.ShippingProvider);
            Assert.NotNull(transaction.Date);
            Assert.Equal(2, transaction.Date.Value.Month);
            Assert.Equal(1, transaction.Date.Value.Day);
            Assert.Equal(2015, transaction.Date.Value.Year);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ShouldNotParseLine_TooFewElements()
        {
            const string input = "2015-02-01 S";
            var transaction = _transactionParser.ParseTransaction(input);

            Assert.NotNull(transaction.CorruptedData);
            Assert.Equal(input, transaction.CorruptedData);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ShouldNotParseLine_UnrecognizedDate()
        {
            const string input = "2blahblah1 S MR";
            var transaction = _transactionParser.ParseTransaction(input);

            Assert.NotNull(transaction.CorruptedData);
            Assert.Equal(input, transaction.CorruptedData);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ShouldNotParseLine_UnrecognizedSize()
        {
            const string input = "2015-02-01 Y MR";
            var transaction = _transactionParser.ParseTransaction(input);

            Assert.NotNull(transaction.CorruptedData);
            Assert.Equal(input, transaction.CorruptedData);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ShouldNotParseLine_UnrecognizedProvider()
        {
            const string input = "2015-02-01 S E?";
            var transaction = _transactionParser.ParseTransaction(input);

            Assert.NotNull(transaction.CorruptedData);
            Assert.Equal(input, transaction.CorruptedData);
        }
    }
}
