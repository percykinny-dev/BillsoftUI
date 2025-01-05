namespace BS.Infrastructure.Repositories.SYS;

public class SysLogRepository : ISysLogRepository
{
    readonly BillsoftDBContext context;

    public SysLogRepository(BillsoftDBContext context)
    {
        this.context = context;
    }

    public async Task<bool> Log(SysLog log)
    {
        //await context.SysLogs.AddAsync(log);
        return true;
    }
}
