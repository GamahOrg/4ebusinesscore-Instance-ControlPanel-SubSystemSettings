namespace UserManagement.Helpers.Extensions
{
    public static class SaudiArTimeZoneExtension
    {
        public static DateTime ToSaudiArabiaTime(this DateTime utcDateTime)
        {
            TimeSpan saudiOffset = TimeSpan.FromHours(3);

            DateTime saudiArabiaTime = utcDateTime + saudiOffset;
            return saudiArabiaTime;
        }
    }
}
