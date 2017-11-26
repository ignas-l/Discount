using System;

namespace Discount.Domain.Objects
{
    public class Transaction
    {
        public DateTime? Date { get; set; }
        public string Size { get; set; }
        public string ShippingProvider { get; set; }
        public decimal? ShippingPrice { get; set; }
        public decimal? Discount { get; set; }
        public string CorruptedData { get; set; }
    }
}
