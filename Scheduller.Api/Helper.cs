namespace Scheduller.Api
{
    public static class Helper
    {
        public static IEnumerable<DateTime> MakeDateRange (DateTime start, DateTime end)
        {
            if (end < start)
                yield break;

            for (var date = start.Date; date <= end.Date; date = date.AddDays(1))
                yield return date;
        }
    }
}
