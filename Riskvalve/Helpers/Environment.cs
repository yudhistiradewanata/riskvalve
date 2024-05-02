public static class Environment
{
    // private static string _DbAldo =
    //     "Server=127.0.0.1,1433;Database=Riskvalve;User Id=SA;Password=DB_Password;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";

    public static string app_path = "/tesvims";
    public static string app_version = "v0.0.25";
    public static string GetConnectionStringDB()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        return configuration?.GetConnectionString("DefaultConnection") ?? "";
    }

    public static string GetDateFormatString(bool withTime = true)
    {
        return withTime ? "dd-MM-yyyy HH:mm:ss" : "dd-MM-yyyy";
    }

    public static string GetAppVersion()
    {
        return app_version;
    }

    public static int StringToInt(string value)
    {
        return int.TryParse(value, out int result) ? result : 0;
    }
}
