namespace CleanCQRS.Infrastructure.Helper
{
    public static class DateTimeExtensions
    {
        public static bool IsIn1MinuteRangeOf(this DateTime source, DateTime target)
        {
            return Math.Abs((source - target).TotalSeconds) <= 60;
        }
    }

}
