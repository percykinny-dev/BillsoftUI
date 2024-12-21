namespace BS.UI.Web.Controllers;

[Route("Login")]
public class LoginController : Controller
{
    string logReference = "bs-login";
    readonly IUserLoginService userLoginService;
    readonly ISysLogService logService;
    readonly INotyfService notificationService;

    public LoginController(
        IUserLoginService userLoginService,
        ISysLogService logService,
        INotyfService notificationService)
    {
        this.userLoginService = userLoginService;
        this.logService = logService;
        this.notificationService = notificationService;
    }


    [HttpGet]
    [Route("")]
    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        return View();
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(string u, string p, bool rm)
    {
        try
        {
            var x = await userLoginService.Login(new LoginVM() { Username = u, Password = p });
            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            var msg = "";
            notificationService.Error(msg);
            logService.Log(logReference, msg, ex);
            return Json(new {success = false});
        }
    }

    [HttpGet]
    [Route("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword()
    {
        return View();
    }

    [HttpGet]
    [Route("SendPasswordResetLink")]
    public async Task<IActionResult> SendPasswordResetLink(string email)
    {
        try
        {
            var forgotPasswordLink = Url.Action("ResetPassword", "Login", null, Request.Scheme);
            var x = await userLoginService.SendPasswordResetLink(email, forgotPasswordLink);

            return Json(new { success = true });
        }
        catch (Exception ex)
        {

            return Json(new { success = false });
        }
    }

    [HttpGet]
    [Route("ResetPassword")]
    public async Task<IActionResult> ResetPassword(string token)
    {
        return View();
    }


    [HttpPost]
    [Route("ResetPassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                resetPasswordVM.NewPassword = resetPasswordVM.ConfirmNewPassword = string.Empty;
                return View(resetPasswordVM);
            }

            var result = await userLoginService.ResetPassword(resetPasswordVM);
            if (!result.IsSuccess)
            {
                notificationService.Error("");
                resetPasswordVM.NewPassword = resetPasswordVM.ConfirmNewPassword = string.Empty;
                return View(resetPasswordVM);
            }

            notificationService.Success("password reset successfully");
            return RedirectToAction("index", "login");
        }
        catch (Exception ex)
        {
            var message = "error resetting password";
            
            notificationService.Error(message);
            resetPasswordVM.NewPassword = resetPasswordVM.ConfirmNewPassword = string.Empty;
            return View(resetPasswordVM);
        }
    }
}
