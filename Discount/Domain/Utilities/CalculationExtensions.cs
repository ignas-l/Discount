using Discount.Domain.Objects;

namespace Discount.Domain.Utilities
{
    public static class CalculationExtensions
    {
        public static Transaction CalculateLargeShipmentDiscount(this Transaction transaction, double discountRemaining)
        {
            return transaction;
        }

        public static Transaction CalculateSmallShipmentDiscount(this Transaction transaction, double discountRemaining)
        {
            return transaction;
        }
    }
}
