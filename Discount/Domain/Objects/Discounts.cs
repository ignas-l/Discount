using System.Collections.Generic;

namespace Discount.Domain.Objects
{
    public class Discounts
    {
        public Transaction Transaction { get; set; }
        public Dictionary<string, decimal> MonthlyDiscounts { get; set; }
    }
}
