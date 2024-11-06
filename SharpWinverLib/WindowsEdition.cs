namespace SharpWinver;

using Core;

public static class WindowsEdition
{
    //获取系统版本名称
    public static string OSEditionName
    {
        get
        {
            string osEdition = string.Empty;
            if (WinBrand.CanInvoke) osEdition = WinBrandInfo.FullWindowsProductString;
            if (string.IsNullOrEmpty(osEdition)) osEdition = WinNTEdition.ProductName;
            if (string.IsNullOrEmpty(osEdition))
            {
                string edtidstr = !string.IsNullOrEmpty(WinNTEdition.EditionID) ? $" {WinNTEdition.EditionID}" : string.Empty;
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
