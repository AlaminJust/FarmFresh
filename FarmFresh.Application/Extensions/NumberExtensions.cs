namespace FarmFresh.Application.Extensions
{
    public static class NumberExtensions
    {
        public static bool IsNonNegative(this short number)
        {
            return number <= 0;
        }

        public static bool IsNonNegative(this int number)
        {
            return number <= 0;
        }

        public static bool IsNonNegative(this long number)
        {
            return number <= 0;
        }

        public static bool IsPositive(this int number)
        {
            return number > 0;
        }
    }
}
