using System.Collections.Generic;
using System.IO;
using Discount.Configuration;
using Discount.Domain.Interfaces;
using Discount.Domain.Objects;

namespace Discount.Domain
{
    public class TransactionReader : ITransactionReader
    {
        private readonly ITransactionParser _transactionParser;

        public TransactionReader(ITransactionParser transactionParser)
        {
            _transactionParser = transactionParser;
        }

        public List<Transaction> ReadTransactions()
        {
            var file = File.ReadAllText(@"Resources\" + Constants.InputFile);

            var transactionList = new List<Transaction>();

            using (var reader = new StringReader(file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var transaction = _transactionParser.ParseTransaction(line);
                    transactionList.Add(transaction);
                }
            }

            return transactionList;
        } 
    }
}
