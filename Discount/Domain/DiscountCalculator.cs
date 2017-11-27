using System.Collections.Generic;
using Discount.Configuration;
using Discount.Domain.Interfaces;
using Discount.Domain.Objects;
using Discount.Domain.Utilities;

namespace Discount.Domain
{
    public class DiscountCalculator : IDiscountCalculator
    {
        public List<Transaction> CalculateDiscounts(List<Transaction> transactions)
        {
            var calculatedTransactions = new List<Transaction>();

            var monthlyDiscounts = GenerateDiscounts(transactions);

            foreach (var transaction in transactions)
            {
                calculatedTransactions.Add(CalculateDiscount(transaction, monthlyDiscounts));
            }

            return calculatedTransactions;
        }

        private static Transaction CalculateDiscount(Transaction transaction, Dictionary<string, decimal> monthlyDiscounts)
        {
            if (transaction.CorruptedData != null)
                return transaction;

            transaction = transaction.AssignShippingPrice();

            var discounts = new Discounts
            {
                Transaction = transaction,
                MonthlyDiscounts = monthlyDiscounts
            };

            discounts = discounts
                .CalculateSmallShipmentDiscount();

            return discounts.Transaction;
        }

        // This method takes in a list of transactions and returns a new dictionary with unique YearMonth keys.
        // Key equals to YearMonth, value equals to remaining monthly discount (defined in Constants).
        private static Dictionary<string, decimal> GenerateDiscounts(IEnumerable<Transaction> transactions)
        {
            var yearMonth = new List<string>();
            foreach (var transaction in transactions)
            {
                if (transaction.CorruptedData != null)
                    continue;

                yearMonth.Add(transaction.Date?.GetYearMonth());
            }

            var uniqueYearMonth = new HashSet<string>(yearMonth);
            var discounts = new Dictionary<string, decimal>();

            foreach (var month in uniqueYearMonth)
            {
                discounts.Add(month, Constants.MonthlyDiscount);
            }

            return discounts;
        }
    }
}
