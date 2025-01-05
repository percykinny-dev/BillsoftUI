namespace BS.Application.Services;

public class UserLoginService : IUserLoginService
{
    public UserLoginService()
    {
        
    }

    public Task<SYSUser> GetUserByEmail(string emailAddress)
    {
        throw new NotImplementedException();
    }

    public Task<SYSUser> GetUserByUsername(string username)
    {
        throw new NotImplementedException();
    }

    public Task<LoggedInUserVM> Login(LoginVM loginVM)
    {
        throw new NotImplementedException();
    }

    public void Logout(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<ResultVM> ResetPassword(ResetPasswordVM resetPasswordVM)
    {
        throw new NotImplementedException();
    }

    public Task<ResultVM> SendPasswordResetLink(string userEmail, string resetPasswordLink)
    {
        throw new NotImplementedException();
    }
}
