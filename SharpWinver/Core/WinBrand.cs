using SharpWinNTInterop;
using System;
using System.Runtime.InteropServices;

namespace SharpWinver.Core;

public static class WinBrand
{
    public const string WinBrandDll = "winbrand.dll";

    [DllImport(WinBrandDll, CharSet = CharSet.Unicode, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern string BrandingFormatString(string format);

    [DllImport(WinBrandDll, CharSet = CharSet.Unicode, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern HANDLE BrandingLoadImage(IntPtr lpszModule, uint uImageId, uint type, int cx, int cy, uint fuLoad);

    public static class VariableNames
    {
        public const string WindowsLong = "%WINDOWS_LONG%";
        public const string WindowsCopyright = "%WINDOWS_COPYRIGHT%";
        public const string WindowsProduct = "%WINDOWS_PRODUCT%";
        public const string WindowsShort = "%WINDOWS_SHORT%";
        public const string MicrosoftCompanyName = "%MICROSOFT_COMPANYNAME%";
        public const string WindowsGeneric = "%WINDOWS_GENERIC%";
    }

    public static bool CanInvoke
    {
        get
        {
            try
            {
                string? tryGetContent = BrandingFormatString(VariableNames.WindowsGeneric);
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
