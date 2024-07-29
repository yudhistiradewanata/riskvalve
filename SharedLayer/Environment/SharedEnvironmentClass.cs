using System.Reflection.Metadata;

namespace SharedLayer;

public static class SharedEnvironment
{
    public const string app_path = "/vims";
    public const string app_version = "v0.24.7.16";
    public static string GetDateFormatString(bool withTime = true)
    {
        return withTime ? "dd-MM-yyyy HH:mm:ss" : "dd-MM-yyyy";
    }

    public static int StringToInt(string value)
    {
        return int.TryParse(value, out int result) ? result : 0;
    }

    public static string GetAppVersion()
    {
        return app_version;
    }
}
