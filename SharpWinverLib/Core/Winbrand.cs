using System;
using System.Runtime.InteropServices;

namespace SharpWinver.Core;

public static class Winbrand
{
    [DllImport("winbrand.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern string BrandingFormatString(string Format);

    public static class Variables
    {
        public const string WindowsLong = "%WINDOWS_LONG%";
        public const string WindowsProduct = "%WINDOWS_PRODUCT%";
        public const string WindowsCopyright = "%WINDOWS_COPYRIGHT%";
        public const string WindowsGeneric = "%WINDOWS_GENERIC%";
        public const string MicrosoftCompanyName = "%MICROSOFT_COMPANYNAME%";
        public const string WindowsShort = "%WINDOWS_SHORT%";
    }

    public static bool CanInvoke
    {
        get
        {
            try
            {
                string? tryGetContent = BrandingFormatString(Variables.WindowsGeneric);
                if (tryGetContent != null) return tryGetContent.Length > 0;
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

public static class WinBrandInfo
{
    public static string FullWindowsProductString
    {
        get => Winbrand.BrandingFormatString(Winbrand.Variables.WindowsLong);
    }

    public static string WindowsCopyrightString
    {
        get => Winbrand.BrandingFormatString(Winbrand.Variables.WindowsCopyright);
    }
}
