using System;
using System.Collections.Generic;
using System.IO;
using Discount.Configuration;
using Discount.Domain;
using Discount.Domain.Interfaces;
using Discount.Domain.Objects;
using Xunit;

namespace Discount.Tests
{
    public class OutputPrinterTests
    {
        private readonly IOutputPrinter _outputPrinter;
        private readonly List<Transaction> _transactions;

        public OutputPrinterTests()
        {
            _outputPrinter = new OutputPrinter();

            _transactions = new List<Transaction>
            {
                new Transaction
                {
                    Size = Constants.Sizes.Medium,
                    Date = DateTime.UtcNow,
                    ShippingPrice = Constants.ShippingPrices.Medium.LaPoste,
                    ShippingProvider = Constants.Providers.LaPoste,
                    Discount = new decimal(0.5)
                },
                new Transaction
                {
                    Size = Constants.Sizes.Small,
                    Date = DateTime.UtcNow,
                    ShippingPrice = Constants.ShippingPrices.Small.MondialRelay,
                    ShippingProvider = Constants.Providers.MondialRelay
                },
                new Transaction
                {
                    Size = Constants.Sizes.Large,
                    Date = DateTime.UtcNow,
                    ShippingPrice = Constants.ShippingPrices.Medium.LaPoste,
                    ShippingProvider = Constants.Providers.LaPoste,
                    Discount = new decimal(2.0),
                    CorruptedData = "CorruptedDataTest"
                }
        };
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ShouldPrintTransaction()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                _outputPrinter.PrintOutput(_transactions);

                Assert.NotNull(sw.ToString());
                Assert.Contains("CorruptedDataTest", sw.ToString());
                Assert.Contains(Constants.Providers.LaPoste, sw.ToString());
                Assert.Contains(Constants.Providers.MondialRelay, sw.ToString());
                Assert.Contains(Constants.Sizes.Medium, sw.ToString());
                Assert.Contains(Constants.Sizes.Small, sw.ToString());
                Assert.Contains(Constants.Sizes.Large, sw.ToString());
            }
        }
    }
}
