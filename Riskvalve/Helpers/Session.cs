namespace Riskvalve;

public static class Session
{
    public static bool IsLogin(HttpContext context)
    {
        if (context.Session.GetString("IsLogin") != null)
        {
            bool status = Convert.ToBoolean(context.Session.GetString("IsLogin"));
            return status;
        }
        return false;
    }

    public static Dictionary<string, string> GetLoginSession(HttpContext context)
    {
        Dictionary<string, string> session =
            new()
            {
                { "IsLogin", context.Session.GetString("IsLogin") ?? "false" },
                { "Username", context.Session.GetString("Username") ?? "" },
                { "Role", context.Session.GetString("Role") ?? "" },
                { "Id", context.Session.GetString("Id") ?? "0" },
                { "IsAdmin", context.Session.GetString("IsAdmin") ?? "false" },
                { "IsEngineer", context.Session.GetString("IsEngineer") ?? "false" },
                { "IsViewer", context.Session.GetString("IsViewer") ?? "false" }
            };
        return session;
    }
}
