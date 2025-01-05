namespace BS.Application.Contracts.SYS;

public interface ISysLogService
{
    void Log(string reference, string message);

    void Log(string reference, string message, Exception ex);
}
