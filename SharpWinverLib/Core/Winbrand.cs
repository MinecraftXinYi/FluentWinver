using System;
using System.Runtime.InteropServices;

namespace SharpWinver.Core;

public static class Winbrand
{
    [DllImport("winbrand.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern string BrandingFormatString(string format);

    public static class PathStrings
    {
        public const string WindowsGeneric = "%WINDOWS_GENERIC%";
        public const string WindowsShort = "%WINDOWS_SHORT%";
        public const string WindowsLong = "%WINDOWS_LONG%";
        public const string WindowsProduct = "%WINDOWS_PRODUCT%";
        public const string WindowsCopyright = "%WINDOWS_COPYRIGHT%";
        public const string MicrosoftCompanyName = "%MICROSOFT_COMPANYNAME%";
    }

    public static bool CanInvoke
    {
        get
        {
            try
            {
                string? tryGetContent = BrandingFormatString(PathStrings.WindowsGeneric);
                if (tryGetContent != null)
                {
                    if (tryGetContent.Length > 0) return true;
                    else return false;
                }
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public static class WinBrandInfo
    {
        public static string WindowsLong
        {
            get => BrandingFormatString(PathStrings.WindowsLong);
        }

        public static string WindowsCopyright
        {
            get => BrandingFormatString(PathStrings.WindowsCopyright);
        }
    }
}
