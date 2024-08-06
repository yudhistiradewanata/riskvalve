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

    public static Dictionary<string, string> CheckPermission(HttpContext context, string page)
    {
        Dictionary<string, string> permission =
            new()
            {
                { "Login", "false" },
                { "Permission", "false" },
                { "Message", "Access Denied" }
            };
        bool IsLogin = Session.IsLogin(context);
        if (!IsLogin)
        {
            permission["Message"] = "Please login to access this page";
        }
        else
        {
            permission["Login"] = "true";
            Dictionary<string, string> CurrentSession = Session.GetLoginSession(context);
            List<string> permittedEngineerPage =
            [
                "AreaRegister",
                "Assessment",
                "Home",
                "Inspection",
                "Maintenance",
                "Tool"
            ];
            List<string> permittedViewerPage = [
                "Home"
            ];
            List<string> permittedAdminPage = [
                "Home",
                "User"
            ];
            List<string> permittedPage = [];
            if(CurrentSession.TryGetValue("IsEngineer", out string? value) && value?.ToString()?.ToLower().Equals("true") == true)
            {
                permittedPage.AddRange(permittedEngineerPage);
            }
            if(CurrentSession.TryGetValue("IsViewer", out string? value2) && value2?.ToString()?.ToLower().Equals("true") == true)
            {
                permittedPage.AddRange(permittedViewerPage);
            }
            if(CurrentSession.TryGetValue("IsAdmin", out string? value3) && value3?.ToString()?.ToLower().Equals("true") == true)
            {
                permittedPage.AddRange(permittedAdminPage);
            }
            if(permittedPage.Contains(page))
            {
                permission["Permission"] = "true";
                permission["Message"] = "Access Granted";
            }
            else
            {
                permission["Message"] = "You are not authorized to access this page";
            }
        }
        return permission;
    }
}
