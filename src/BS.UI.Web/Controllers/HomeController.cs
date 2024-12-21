namespace BS.UI.Web.Controllers;

[Route("")]
[Route("[controller]")]
public class HomeController : Controller
{

    public HomeController()
    {
        
    }

    [HttpGet]
    [Route("")]
    [Route("Index")]
    public IActionResult Index()
    {
        return View();
    }
}
