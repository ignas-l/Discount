using System;
using System.Collections.Generic;
using System.Text;
using Discount.Configuration;
using Discount.Domain.Interfaces;
using Discount.Domain.Objects;
using Discount.Domain.Utilities;

namespace Discount.Domain
{
    public class DiscountCalculator : IDiscountCalculator
    {
        private decimal _discountRemaining;

        public List<Transaction> CalculateDiscounts(List<Transaction> transactions)
        {
            var calculatedTransactions = new List<Transaction>();
            _discountRemaining = Constants.MonthlyDiscount;

            foreach (var transaction in transactions)
            {
                calculatedTransactions.Add(CalculateDiscount(transaction));
            }

            return calculatedTransactions;
        }

        private static Transaction CalculateDiscount(Transaction transaction)
        {
            return transaction
                .CalculateLargeShipmentDiscount()
                .CalculateSmallShipmentDiscount();
        }
    }
}
