namespace BS.Application.ViewModels.Login;

public class ResetPasswordVM
{

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Confirm Password is required")]
    [Compare("NewPassword", ErrorMessage = "New Password and Confirm Password should match")]
    public string ConfirmNewPassword { get; set; }
}
