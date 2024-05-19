using System.Runtime.InteropServices;

namespace RTWinver.Services;

internal static class WinbrandService
{
    [DllImport("winbrand.dll", CharSet = CharSet.Unicode)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    static extern string BrandingFormatString(string format);


    public static string WindowsLong
    {
        get => BrandingFormatString("%WINDOWS_LONG%");
    }
}
