namespace BS.Application.Services.SYS;

public class SysLogService : ISysLogService
{
    readonly ISysLogRepository logRepository;

    public SysLogService(ISysLogRepository logRepository)
    {
        this.logRepository = logRepository;
    }

    public void Log(string reference, string message)
    {
        var logMessage = $"msg:{message}";
        var logData = new SysLog
        {
            LogType = "Info",
            CreatedOn = DateTime.Now,
            Category = reference,
            Message = logMessage
        };
        logRepository.Log(logData);
    }

    public void Log(string reference, string message, Exception ex)
    {
        var logMessage = $"msg:{message} - ex:{ex.Message} st:{ex.StackTrace}";
        var logData = new SysLog
        {
            LogType = "Error",
            CreatedOn = DateTime.Now,
            Category = reference,
            Message = logMessage
        };
        logRepository.Log(logData);
    }
}
