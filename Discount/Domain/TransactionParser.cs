using System;
using System.Collections.Generic;
using Discount.Configuration;
using Discount.Domain.Interfaces;
using Discount.Domain.Objects;
using Discount.Domain.Utilities;

namespace Discount.Domain
{
    public class TransactionParser : ITransactionParser
    {
        public Transaction ParseTransaction(string line)
        {
            line = line.CleanWhiteSpace();

            var elementList = new List<string>(line.Split(Constants.Separator));

            var transaction = new Transaction();

            // Each line should contain date, size and provider separated by a given separator (empty space).
            if (elementList.Count < 3)
            {
                transaction.CorruptedData = line;
                return transaction;
            }

            if (!DateTime.TryParse(elementList[0], out var date))
            {
                transaction.CorruptedData = line;
                return transaction;
            }

            transaction.Date = date;
            transaction.Size = elementList[1];
            transaction.ShippingProvider = elementList[2];

            if (!ValidateConstants(transaction))
            {
                transaction.CorruptedData = line;
                return transaction;
            }

            return transaction;
        }

        // Checks if given transaction's size and provider contain valid values.
        private static bool ValidateConstants(Transaction transaction)
        {
            var sizeConstants = Constants.GetConstants(typeof(Constants.Sizes));
            var providerConstants = Constants.GetConstants(typeof(Constants.Providers));

            if (sizeConstants.Contains(transaction.Size) && providerConstants.Contains(transaction.ShippingProvider))
            {
                return true;
            }

            return false;
        }
    }
}
