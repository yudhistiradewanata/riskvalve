using System.Reflection.Metadata;

namespace SharedLayer;

public static class SharedEnvironment
{
    public const string app_path = "/vims";
    public const string app_version = "v0.24.8.8";

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
        string[] permittedImageExtensions =
        [
            ".ase",
            ".art",
            ".bmp",
            ".blp",
            ".cd5",
            ".cit",
            ".cpt",
            ".cr2",
            ".cut",
            ".dds",
            ".dib",
            ".djvu",
            ".egt",
            ".exif",
            ".gif",
            ".gpl",
            ".grf",
            ".icns",
            ".ico",
            ".iff",
            ".jng",
            ".jpeg",
            ".jpg",
            ".jfif",
            ".jp2",
            ".jps",
            ".lbm",
            ".max",
            ".miff",
            ".mng",
            ".msp",
            ".nitf",
            ".ota",
            ".pbm",
            ".pc1",
            ".pc2",
            ".pc3",
            ".pcf",
            ".pcx",
            ".pdn",
            ".pgm",
            ".PI1",
            ".PI2",
            ".PI3",
            ".pict",
            ".pct",
            ".pnm",
            ".pns",
            ".ppm",
            ".psb",
            ".psd",
            ".pdd",
            ".psp",
            ".px",
            ".pxm",
            ".pxr",
            ".qfx",
            ".raw",
            ".rle",
            ".sct",
            ".sgi",
            ".rgb",
            ".int",
            ".bw",
            ".tga",
            ".tiff",
            ".tif",
            ".vtf",
            ".xbm",
            ".xcf",
            ".xpm",
            ".3dv",
            ".amf",
            ".ai",
            ".awg",
            ".cgm",
            ".cdr",
            ".cmx",
            ".dxf",
            ".e2d",
            ".egt",
            ".eps",
            ".fs",
            ".gbr",
            ".odg",
            ".svg",
            ".stl",
            ".vrml",
            ".x3d",
            ".sxd",
            ".v2d",
            ".vnd",
            ".wmf",
            ".emf",
            ".art",
            ".xar",
            ".png",
            ".webp",
            ".jxr",
            ".hdp",
            ".wdp",
            ".cur",
            ".ico",
            ".info",
            ".icon",
            ".ani"
        ];
        string[] permittedExcelExtensions = [".xls", ".xlsx"];

        string[] permittedExtensions = [.. permittedImageExtensions, .. permittedExcelExtensions];
        return [.. permittedExtensions];
    }
}
