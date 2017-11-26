namespace Discount.Configuration
{
    public static class Constants
    {
        public static string InputFile = "input.txt";
        public static string ErrorText = "Ignored";
        public static string NoDiscount = "-";
        public static string Separator = " ";
        public static string CurrencyFormat = "0.00";
        public static decimal MonthlyDiscount = new decimal(10.00);

        public static class Sizes
        {
            public static string Small = "S";
            public static string Medium = "M";
            public static string Large = "L";
        }

        public static class Providers
        {
            public static string MondialRelay = "MR";
            public static string LaPoste = "LP";
        }

        public static class ShippingPrices
        {
            public static decimal MondialRelaySmall = new decimal(2.00);
            public static decimal MondialRelayMedium = new decimal(3.00);
            public static decimal MondialRelayLarge = new decimal(4.00);

            public static decimal LaPosteSmall = new decimal(1.50);
            public static decimal LaPosteMedium = new decimal(4.90);
            public static decimal LaPosteLarge = new decimal(6.90);
        }
    }
}
