namespace SharpWinver;

using Core;

public class WindowsEdition
{
    //获取系统类型名称
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
                osEdition = WinEdition.WinProductName;
                if (string.IsNullOrEmpty(osEdition))
                    osEdition = $"Microsoft Windows {WinEdition.EditionID}";
            }
            return osEdition;
        }
    }

    //检测系统是否为桌面端 Windows
    public static bool IsDesktopEdition
    {
        get
        {
            return WinEdition.IsDesktopPlatform;
        }
    }

    //获取系统的平台架构
    public static string OSArchitecture
    {
        get
        {
            return WinEdition.PlatformArchitecture;
        }
    }
}
