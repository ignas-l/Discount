using System.Linq;
using Discount.Configuration;
using Discount.Domain.Objects;

namespace Discount.Domain.Utilities
{
    public static class CalculationExtensions
    {
        public static Discounts AssignShippingPrices(this Discounts discounts)
        {
            foreach (var transaction in discounts.Transactions)
            {
                if (!IsTransactionValid(transaction))
                    continue;

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
            }

            return discounts;
        }

        public static Discounts CalculateSmallShipmentDiscounts(this Discounts discounts)
        {
            var lowestPrice = FindLowestSmallShippingPrice();

            foreach (var transaction in discounts.Transactions)
            {
                if (!IsTransactionValid(transaction))
                    continue;

                var yearMonth = transaction.Date?.GetYearMonth();
                discounts.MonthlyDiscounts.TryGetValue(yearMonth, out var remainingDiscount);

                if (transaction.Size == Constants.Sizes.Small && remainingDiscount > 0 && lowestPrice < transaction.ShippingPrice)
                {
                    var amountToDiscount = transaction.ShippingPrice - lowestPrice;

                    if (amountToDiscount <= remainingDiscount) // Sufficient discounts remaining.
                    {
                        transaction.Discount = amountToDiscount;
                        transaction.ShippingPrice = lowestPrice;
                        remainingDiscount -= amountToDiscount.Value;
                    }
                    else // Insufficient discounts, applying what's left.
                    {
                        transaction.Discount = remainingDiscount;
                        transaction.ShippingPrice -= remainingDiscount;
                        remainingDiscount = 0;
                    }

                    discounts.MonthlyDiscounts.Remove(yearMonth);
                    discounts.MonthlyDiscounts.Add(yearMonth, remainingDiscount);
                }
            }

            return discounts;
        }

        public static Discounts CalculateLargeShipmentDiscounts(this Discounts discounts)
        {
            ushort largeCounter = 0;
            foreach (var transaction in discounts.Transactions)
            {
                if (!IsTransactionValid(transaction))
                    continue;

                if (transaction.Size == Constants.Sizes.Large)
                    largeCounter++;

                if (largeCounter.Equals(3))
                {
                    var yearMonth = transaction.Date?.GetYearMonth();
                    discounts.MonthlyDiscounts.TryGetValue(yearMonth, out var remainingDiscount);

                    if (remainingDiscount > 0)
                    {
                        if (remainingDiscount >= transaction.ShippingPrice) // Sufficient discounts remaining.
                        {
                            remainingDiscount -= transaction.ShippingPrice.Value;
                            transaction.Discount = transaction.ShippingPrice;
                            transaction.ShippingPrice = 0;
                        }
                        else // Insufficient discounts, applying what's left.
                        {
                            transaction.ShippingPrice -= remainingDiscount;
                            transaction.Discount = remainingDiscount;
                            remainingDiscount = 0;
                        }

                        discounts.MonthlyDiscounts.Remove(yearMonth);
                        discounts.MonthlyDiscounts.Add(yearMonth, remainingDiscount);
                    }

                    largeCounter = 0;
                }
            }
            return discounts;
        }

        private static bool IsTransactionValid(Transaction transaction)
        {
            return transaction.CorruptedData == null;
        }

        private static decimal FindLowestSmallShippingPrice()
        {
            var shippingPrices = Constants.GetConstants(typeof(Constants.ShippingPrices.Small));
            return (decimal)shippingPrices.Min();
        }
    }
}
