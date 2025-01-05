namespace BS.Application.Contracts;

public interface IUserLoginService
{
    Task<LoggedInUserVM> Login(LoginVM loginVM);

    void Logout(int userId);

    Task<SYSUser> GetUserByEmail(string emailAddress);

    Task<SYSUser> GetUserByUsername(string username);

    Task<ResultVM> SendPasswordResetLink(string userEmail, string resetPasswordLink);

    Task<ResultVM> ResetPassword(ResetPasswordVM resetPasswordVM);
}