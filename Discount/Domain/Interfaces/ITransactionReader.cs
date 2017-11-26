using System.Collections.Generic;
using Discount.Domain.Objects;

namespace Discount.Domain.Interfaces
{
    public interface ITransactionReader
    {
        List<Transaction> ReadTransactions();
    }
}
