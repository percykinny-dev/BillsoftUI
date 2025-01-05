namespace BS.Application.Repositories.SYS;

public interface ISysLogRepository
{
    Task<bool> Log(SysLog log);
}
