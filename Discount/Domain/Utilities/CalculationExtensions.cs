using System.Collections.Generic;
using System.Linq;
using Discount.Configuration;
using Discount.Domain.Objects;

namespace Discount.Domain.Utilities
{
    public static class CalculationExtensions
    {
        public static Discounts CalculateSmallShipmentDiscount(this Discounts discounts)
        {
            var yearMonth = discounts.Transaction.Date?.GetYearMonth();
            discounts.MonthlyDiscounts.TryGetValue(yearMonth, out var remainingDiscount);

            if (discounts.Transaction.Size == Constants.Sizes.Small && remainingDiscount > 0)
            {
                var lowestPrice = FindLowestSmallShippingPrice();

                if (lowestPrice < discounts.Transaction.ShippingPrice && remainingDiscount > 0)
                {
                    var amountToDiscount = discounts.Transaction.ShippingPrice - lowestPrice;
                    if (amountToDiscount <= remainingDiscount) // Sufficient discounts remaining.
                    {
                        discounts.Transaction.Discount = amountToDiscount;
                        discounts.Transaction.ShippingPrice = lowestPrice;
                        remainingDiscount -= amountToDiscount.Value;
                    }
                    else // Insufficient discounts, applying what's left.
                    {
                        discounts.Transaction.Discount = remainingDiscount;
                        discounts.Transaction.ShippingPrice -= amountToDiscount;
                        remainingDiscount = 0;
                    }

                    discounts.MonthlyDiscounts.Remove(yearMonth);
                    discounts.MonthlyDiscounts.Add(yearMonth, remainingDiscount);
                }
            }

            return discounts;
        }

        public static Transaction CalculateLargeShipmentDiscount(this Transaction transaction, Dictionary<string, decimal> monthlyDiscounts)
        {
            return transaction;
        }

        // Assigns shipping price according to size and provider.
        public static Transaction AssignShippingPrice(this Transaction transaction)
        {
            if (transaction.Size == Constants.Sizes.Small)
            {
                if (transaction.ShippingProvider == Constants.Providers.LaPoste)
                {
                    transaction.ShippingPrice = Constants.ShippingPrices.Small.LaPoste;
                }
                else if (transaction.ShippingProvider == Constants.Providers.MondialRelay)
                {
                    transaction.ShippingPrice = Constants.ShippingPrices.Small.MondialRelay;
                }
            }
            else if (transaction.Size == Constants.Sizes.Medium)
            {
                if (transaction.ShippingProvider == Constants.Providers.LaPoste)
                {
                    transaction.ShippingPrice = Constants.ShippingPrices.Medium.LaPoste;
                }
                else if (transaction.ShippingProvider == Constants.Providers.MondialRelay)
                {
                    transaction.ShippingPrice = Constants.ShippingPrices.Medium.MondialRelay;
                }
            }
            else if (transaction.Size == Constants.Sizes.Large)
            {
                if (transaction.ShippingProvider == Constants.Providers.LaPoste)
                {
                    transaction.ShippingPrice = Constants.ShippingPrices.Large.LaPoste;
                }
                else if (transaction.ShippingProvider == Constants.Providers.MondialRelay)
                {
                    transaction.ShippingPrice = Constants.ShippingPrices.Large.MondialRelay;
                }
            }

            return transaction;
        }

        private static decimal FindLowestSmallShippingPrice()
        {
            var shippingPrices = Constants.GetConstants(typeof(Constants.ShippingPrices.Small));
            return (decimal)shippingPrices.Min();
        }
    }
}
