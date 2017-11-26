using Discount.Domain.Objects;

namespace Discount.Domain.Interfaces
{
    public interface ITransactionParser
    {
        Transaction ParseTransaction(string line);
    }
}
