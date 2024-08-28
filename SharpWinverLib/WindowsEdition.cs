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
            if (Winbrand.CanInvoke)
            {
                osEdition = Winbrand.WinBrandInfo.WindowsLong;
            }
            else
            {
                osEdition = WinNTEdition.ProductName;
                if (string.IsNullOrEmpty(osEdition))
                    osEdition = $"Microsoft Windows {WinNTEdition.EditionID}";
            }
            return osEdition;
        }
    }

    //检测系统是否为桌面端 Windows
    public static bool IsDesktopEdition
    {
        get
        {
            return WinNTEdition.IsDesktopPlatform;
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
