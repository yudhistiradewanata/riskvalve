using System.Reflection.Metadata;
using System.Text.Encodings.Web;
using System.Web;

namespace SharedLayer;

public static class SharedEnvironment
{
    public const string app_path = "/vims";
    public const string app_version = "v0.24.9.1";

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

    public static List<string> GetPermittedExtension()
    {
        string[] permittedImageExtensions =[".jpg", ".jpeg", ".bmp", ".png"];
        // string[] permittedExcelExtensions = [".xls", ".xlsx"];
        // string[] permittedExtensions = [.. permittedImageExtensions, .. permittedExcelExtensions];
        return [.. permittedImageExtensions];
    }

    public static string? HtmlEncode(string? value){
        if (string.IsNullOrEmpty(value)) return string.Empty;
        string encoded = HttpUtility.HtmlEncode(value);
        encoded = encoded.Replace("&quot;", "\"").Replace("&amp;", "&");
        return encoded;
    }
}
