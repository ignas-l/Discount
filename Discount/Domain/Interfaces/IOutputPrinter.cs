using System.Collections.Generic;
using Discount.Domain.Objects;

namespace Discount.Domain.Interfaces
{
    public interface IOutputPrinter
    {
        void PrintOutput(List<Transaction> transactions);
    }
}
