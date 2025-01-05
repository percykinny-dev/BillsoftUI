namespace BS.Application.Contracts.Common;

public interface IDateService
{
    DateTime GetDateTime();

    DateTime GetDateTimeUTC();

    DateTimeOffset GetDateTimeOffsetUTC();
}