namespace BS.UI.Web.Controllers;

public abstract class BSBaseController<T> : Controller where T : BSBaseController<T>
{
    private int _bsUserId = 0;
    private int _bsCompanyId = 0;
    private BSUserSession _bsUser = null;
    private ISysLogService _logService;

    protected ISession session
        => HttpContext.RequestServices.GetService<IHttpContextAccessor>().HttpContext.Session;

    protected int BSUserID
    {
        get
        {
            if (_bsUserId == 0)
            {
                _bsUserId = session.GetUserId();
            }
            return _bsUserId;
        }
    }

    protected int BSCompanyId
    {
        get
        {
            if (_bsCompanyId == 0)
            {
                _bsCompanyId = 2; // session.GetCompanyId();
            }
            return _bsCompanyId;
        }
    }

    protected BSUserSession BSUser
    {
        get
        {
            if (_bsUser == null)
            {
                _bsUser = session.GetUser();
            }
            return _bsUser;
        }
    }
    protected ISysLogService logService
    => _logService ??= HttpContext.RequestServices.GetService<ISysLogService>();
    public BSBaseController()
    {
    }

    protected void LogException(string message, Exception ex)
    {
        logService.Log( message, ex.Message);
    }

    protected static async Task<string> RenderViewToStringAsync(
    string viewName, object model,
    ControllerContext controllerContext,
    Dictionary<string, string> resources = null,
    bool isPartial = false)
    {
        var actionContext = controllerContext as ActionContext;

        var serviceProvider = controllerContext.HttpContext.RequestServices;
        var razorViewEngine = serviceProvider.GetService(typeof(IRazorViewEngine)) as IRazorViewEngine;
        var tempDataProvider = serviceProvider.GetService(typeof(ITempDataProvider)) as ITempDataProvider;
        Dictionary<string, string> _resources = resources;

        using (var sw = new StringWriter())
        {
            var viewResult = razorViewEngine.FindView(actionContext, viewName, !isPartial);

            if (viewResult?.View == null)
                throw new ArgumentException($"{viewName} does not match any available view");

            var viewDictionary =
                new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = model, };

            if (resources != null)
                viewDictionary.Add("Resources", resources);

            var viewContext = new ViewContext(
                actionContext,
                viewResult.View,
                viewDictionary,
                new TempDataDictionary(actionContext.HttpContext, tempDataProvider),
                sw,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);
            return sw.ToString();
        }
    }



}
