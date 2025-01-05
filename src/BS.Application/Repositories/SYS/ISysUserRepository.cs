namespace BS.Application.Repositories.SYS;

public interface ISysUserRepository
{
    Task<SYSUser> GetByUsername(string username);

    Task<SYSUser> GetByUsernamePassword(string username, string password);

    Task<SYSUser> GetUserByEmailUsername(string emailAddress);
}