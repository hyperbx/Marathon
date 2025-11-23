namespace Marathon.Shared
{
    public static class SystemExtensions
    {
        public static string FormatHoursMinutesSeconds(this TimeSpan in_timeSpan)
        {
            if (in_timeSpan.TotalHours >= 1)
            {
                // h:mm:ss.fff
                return string.Format
                (
                    "{0}:{1:00}:{2:00}.{3:000}",
                    in_timeSpan.TotalHours,
                    in_timeSpan.Minutes,
                    in_timeSpan.Seconds,
                    in_timeSpan.Milliseconds
                );
            }
            else if (in_timeSpan.TotalMinutes >= 1)
            {
                // m:ss.fff
                return string.Format
                (
                    "{0}:{1:00}.{2:000}",
                    in_timeSpan.Minutes,
                    in_timeSpan.Seconds,
                    in_timeSpan.Milliseconds
                );
            }
            else if (in_timeSpan.TotalSeconds >= 1)
            {
                // s.fffs
                return string.Format("{0}.{1:000}s", in_timeSpan.Seconds, in_timeSpan.Milliseconds);
            }
            else
            {
                return $"{in_timeSpan.Milliseconds}ms";
            }
        }
    }
}
