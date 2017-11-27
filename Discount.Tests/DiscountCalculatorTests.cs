using System;
using System.Collections.Generic;
using Discount.Configuration;
using Discount.Domain;
using Discount.Domain.Interfaces;
using Discount.Domain.Objects;
using Discount.Domain.Utilities;
using Xunit;

namespace Discount.Tests
{
    public class DiscountCalculatorTests
    {
        private readonly Discounts _discounts;
        private readonly IDiscountCalculator _discountCalculator;

        public DiscountCalculatorTests()
        {
            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    Size = Constants.Sizes.Medium,
                    Date = new DateTime(2017, 05, 10),
                    ShippingProvider = Constants.Providers.LaPoste,
                    Discount = new decimal(0.5)
                },
                new Transaction
                {
                    Size = Constants.Sizes.Small,
                    Date = DateTime.UtcNow,
                    ShippingProvider = Constants.Providers.MondialRelay
                },
                new Transaction
                {
                    Size = Constants.Sizes.Large,
                    Date = DateTime.UtcNow,
                    ShippingProvider = Constants.Providers.LaPoste,
                    CorruptedData = "CorruptedDataTest"
                },
                new Transaction
                {
                    Size = Constants.Sizes.Large,
                    Date = new DateTime(2017, 05, 20),
                    ShippingProvider = Constants.Providers.MondialRelay
                },
                new Transaction
                {
                    Size = Constants.Sizes.Small,
                    Date = new DateTime(2017, 04, 20),
                    ShippingProvider = Constants.Providers.LaPoste
                },
                new Transaction
                {
                    Size = Constants.Sizes.Large,
                    Date = new DateTime(2017, 05, 20),
                    ShippingProvider = Constants.Providers.MondialRelay
                },
                new Transaction
                {
                    Size = Constants.Sizes.Large,
                    Date = new DateTime(2017, 05, 20),
                    ShippingProvider = Constants.Providers.MondialRelay
                }
            };
            _discounts = new Discounts
            {
                Transactions = transactions
            };

            _discountCalculator = new DiscountCalculator();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ShouldAssignShippingPrices()
        {
            _discounts.AssignShippingPrices();
            Assert.Equal(Constants.ShippingPrices.Medium.LaPoste, _discounts.Transactions[0].ShippingPrice);
            Assert.Equal(Constants.ShippingPrices.Small.MondialRelay, _discounts.Transactions[1].ShippingPrice);
            Assert.Null(_discounts.Transactions[2].ShippingPrice);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ShouldCalculateDiscounts()
        {
            var result = _discountCalculator.CalculateDiscounts(_discounts.Transactions);

            var expectedSmallDiscount = Constants.ShippingPrices.Small.MondialRelay - Constants.ShippingPrices.Small.LaPoste;
            Assert.Equal(expectedSmallDiscount, result[1].Discount);
            Assert.Equal(0, result[6].ShippingPrice);
            Assert.Null(result[2].Discount);
        }
    }
}
