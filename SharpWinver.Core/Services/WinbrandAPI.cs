using System;
using System.Runtime.InteropServices;

namespace SharpWinver.Services;

internal static class WinbrandAPI
{
    [DllImport("winbrand.dll", CharSet = CharSet.Unicode)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    static extern string BrandingFormatString(string format);

    public static bool CanInvoke
    {
        get
        {
            bool canInvoke;
            try
            {
                string? tryGetContent;
                tryGetContent = BrandingFormatString("%WINDOWS_GENERIC%");
                if (tryGetContent != null)
                {
                    if (tryGetContent.Length > 0) canInvoke = true;
                    else canInvoke = false;
                }
                else canInvoke = false;
            }
            catch (Exception)
            {
                canInvoke = false;
            }
            return canInvoke;
        }
    }

    public static class GetWinBrandInfo
    {
        public static string WindowsLong
        {
            get => BrandingFormatString("%WINDOWS_LONG%");
        }

        public static string WindowsCopyRight
        {
            get => BrandingFormatString("%WINDOWS_COPYRIGHT%");
        }
    }
}
