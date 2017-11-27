using System.Collections.Generic;

namespace Discount.Domain.Objects
{
    public class Discounts
    {
        public List<Transaction> Transactions { get; set; }
        public Dictionary<string, decimal> MonthlyDiscounts { get; set; }
    }
}
