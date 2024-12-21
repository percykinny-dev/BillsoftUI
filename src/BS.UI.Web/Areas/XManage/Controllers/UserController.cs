namespace BS.UI.Web.Areas.XManage.Controllers;

[Route("[controller]")]
public class UserController : BSBaseController<UserController>
{
    public UserController()
    {

    }


    [HttpGet]
    [Route("")]
    [Route("Index")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [Route("ChangePassword")]
    public IActionResult ChangePassword()
    {
        return View();
    }
}
