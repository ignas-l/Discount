using System;
using System.Collections.Generic;
using System.Reflection;

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
            public static class Small
            {
                public static decimal MondialRelay = new decimal(2.00);
                public static decimal LaPoste = new decimal(1.50);
            }

            public static class Medium
            {
                public static decimal MondialRelay = new decimal(3.00);
                public static decimal LaPoste = new decimal(4.90);
            }

            public static class Large
            {
                public static decimal MondialRelay = new decimal(4.00);
                public static decimal LaPoste = new decimal(6.90);
            }
        }

        public static List<object> GetConstants(Type type)
        {
            // Gets all static and public members from a given type (and its base types as well).
            var fieldInfos = type.GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy);

            var values = new List<object>();

            // Iterates through the created list of static and public members and returns their values.
            foreach (var fieldInfo in fieldInfos)
            {
                values.Add(type.GetField(fieldInfo.Name).GetValue(null));
            }

            return values;
        }
    }
}
