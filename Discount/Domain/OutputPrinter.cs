using System;
using System.Collections.Generic;
using Discount.Configuration;
using Discount.Domain.Interfaces;
using Discount.Domain.Objects;

namespace Discount.Domain
{
    public class OutputPrinter : IOutputPrinter
    {
        public void PrintOutput(List<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                PrintTransaction(transaction);
            }
        }

        private static void PrintTransaction(Transaction transaction)
        {
            if (transaction.CorruptedData == null)
            {
                var discount = transaction.Discount > 0 ? transaction.Discount.Value.ToString(Constants.CurrencyFormat) : Constants.NoDiscount;

                var line = $"{transaction.Date?.Year}-{transaction.Date?.Month}-{transaction.Date?.Day} " +
                           $"{transaction.Size} {transaction.ShippingPrice?.ToString(Constants.CurrencyFormat)} {discount}";

                Console.WriteLine(line);
            }
            else
            {
                Console.WriteLine($"{transaction.CorruptedData} {Constants.ErrorText}");
            }
        }
    }
}
