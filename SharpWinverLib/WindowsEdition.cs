namespace SharpWinver;

using Core;

public static partial class Winver
{
    //获取系统版本名称
    public static string WindowsEdition
    {
        get
        {
            string osEdition = string.Empty;
            if (WinBrand.CanInvoke) osEdition = WinBrand.BrandingFormatString(WinBrand.VariableNames.WindowsLong);
            if (string.IsNullOrEmpty(osEdition)) osEdition = WinNTEdition.ProductName ?? string.Empty;
            if (string.IsNullOrEmpty(osEdition))
            {
                string edtidstr = WinNTEdition.EditionID != null ? $" {WinNTEdition.EditionID}" : string.Empty;
                osEdition = "Windows" + edtidstr;
            }
            return osEdition;
        }
    }

    //获取系统的平台架构
    public static string OSArchitecture
    {
        get
        {
            return WinNTEdition.PlatformArchitecture;
        }
    }
}
