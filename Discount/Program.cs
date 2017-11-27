using System;
using Discount.Domain;

namespace Discount
{
    internal class Program
    {
        private static void Main()
        {
            var transactionParser = new TransactionParser();
            var transactionReader = new TransactionReader(transactionParser);

            var transactions = transactionReader.ReadTransactions();

            var discountCalculator = new DiscountCalculator();
            transactions = discountCalculator.CalculateDiscounts(transactions);

            var outputPrinter = new OutputPrinter();
            outputPrinter.PrintOutput(transactions);

            Console.ReadKey();
        }
    }
}
