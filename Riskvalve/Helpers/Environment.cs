public static class Environment
{
    private static string _DbAldo =
        "Server=127.0.0.1,1433;Database=Riskvalve;User Id=SA;Password=DB_Password;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";

    public static string GetConnectionStringDB()
    {
        return _DbAldo;
    }

    public static string GetDateFormatString()
    {
        return "dd-MM-yyyy HH:mm:ss";
    }
}
