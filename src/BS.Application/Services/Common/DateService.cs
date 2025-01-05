namespace BS.Application.Services.Common;

public class DateService : IDateService
{
    public DateTime GetDateTime()
    {
        return DateTime.Now;
    }

    public DateTime GetDateTimeUTC()
    {
        return DateTime.UtcNow;
    }

    public DateTimeOffset GetDateTimeOffsetUTC()
    {
        return DateTimeOffset.UtcNow;
    }
}
