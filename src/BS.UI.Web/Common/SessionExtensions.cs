namespace BS.UI.Web.Common;

public static class SessionExtensions
{
    //session keys
    private const string user_id = "bs_user_id";
    private const string user_company_id = "bs_user_company_id";
    private const string user_session_data = "bs_user_session_data";
    private const string user_menu_data = "bs_user_menu_data";

    public static void SetUserId(this ISession session, int userId)
    {
        session.SetInt32(user_id, userId);
    }

    public static void SetUser(this ISession session, BSUserSession userSession)
    {
        session.SetString(user_session_data, JsonConvert.SerializeObject(userSession));
    }

    public static void SetUserSidebarMenu(this ISession session, string sidebarMenu)
    {
        session.SetString(user_menu_data, sidebarMenu);
    }

    public static int GetUserId(this ISession session)
    {
        var userId = session.GetInt32(user_id);
        return userId.HasValue ? userId.Value : 0;
    }

    public static int GetUserCompanyId(this ISession session)
    {
        var CompanyID = session.GetInt32(user_company_id);
        return CompanyID.HasValue ? CompanyID.Value : 0;
    }

    public static BSUserSession GetUser(this ISession session)
    {
        var data = session.GetString(user_session_data);
        return string.IsNullOrEmpty(data) ? default : JsonConvert.DeserializeObject<BSUserSession>(data);
    }

    public static bool ClearUserSession(this ISession session)
    {
        try
        {
            Guard.AgainstNull(session);
            session.Remove(user_id);
            session.Remove(user_company_id);
            session.Remove(user_session_data);
            session.Remove(user_menu_data);
            session.Clear();
            return true;
        }
        catch
        {
            return false;
        }
    }
}

