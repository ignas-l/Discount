using System.Text.RegularExpressions;

namespace Discount.Domain.Utilities
{
    public static class StringExtensions
    {
        public static string CleanWhiteSpace(this string str)
        {
            return Regex.Replace(str, @"\s+", " ").TrimStart();
        }
    }
}
