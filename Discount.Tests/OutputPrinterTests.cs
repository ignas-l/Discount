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
                    ShippingPrice = Constants.ShippingPrices.LaPosteMedium,
                    ShippingProvider = Constants.Providers.LaPoste,
                    Discount = new decimal(0.5)
                },
                new Transaction
                {
                    Size = Constants.Sizes.Small,
                    Date = DateTime.UtcNow,
                    ShippingPrice = Constants.ShippingPrices.MondialRelaySmall,
                    ShippingProvider = Constants.Providers.MondialRelay
                },
                new Transaction
                {
                    Size = Constants.Sizes.Large,
                    Date = DateTime.UtcNow,
                    ShippingPrice = Constants.ShippingPrices.LaPosteMedium,
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
            }
        }
    }
}
