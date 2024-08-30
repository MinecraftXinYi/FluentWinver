namespace SharpWinver;

using Core;

public class WindowsEdition
{
    //获取系统版本名称
    public static string OSEditionName
    {
        get
        {
            string osEdition;
            if (WinNTEdition.IsWin32DesktopSupported && Winbrand.CanInvoke)
            {
                osEdition = Winbrand.WinBrandInfo.WindowsLong;
            }
            else
            {
                osEdition = WinNTEdition.ProductName;
                if (string.IsNullOrEmpty(osEdition))
                {
                    string edtidstr = !string.IsNullOrEmpty(WinNTEdition.EditionID) ? $" {WinNTEdition.EditionID}" : string.Empty;
                    osEdition = "Windows" + edtidstr;
                }
            }
            return osEdition;
        }
    }

    //检测系统是否为桌面端 Windows
    public static bool IsWin32Edition
    {
        get
        {
            return WinNTEdition.IsWin32DesktopSupported;
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
