namespace BS.Infrastructure.Repositories.SYS;

public class SysUserRepository : ISysUserRepository
{
    readonly BillsoftDBContext context;

    public SysUserRepository(BillsoftDBContext context)
    {
        this.context = context;
    }

    public async Task<SYSUser> GetByUsername(string username)
    {
        //return await context.SysUsers.FirstOrDefaultAsync(x => x.UserName == username);
        return null;
    }

    public Task<SYSUser> GetByUsernamePassword(string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<SYSUser> GetUserByEmailUsername(string emailAddress)
    {
        //return await context.SysUsers.FirstOrDefaultAsync(x => x.EmailAddress == emailAddress);
        return null;
    }
}
