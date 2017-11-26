using System.Collections.Generic;
using Discount.Domain.Objects;

namespace Discount.Domain.Interfaces
{
    public interface IDiscountCalculator
    {
        List<Transaction> CalculateDiscounts(List<Transaction> transactions);
    }
}
