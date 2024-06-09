namespace RTWinver
{
    using Services;
    using L3Services;

    public static partial class OSEdition
    {
        //获取系统类型名称
        public static string OSEditionName
        {
            get
            {
                string osEdition;
                if (WinbrandAPI.CanInvoke)
                {
                    osEdition = WinbrandAPI.GetWinBrandInfo.WindowsLong;
                }
                else
                {
                    if (OSTypeInfo.IsDesktopPlatform)
                    {
                        osEdition = SpareOSEditionMethods.CreateDesktopEditionName;
                    }
                    else
                    {
                        osEdition = SpareOSEditionMethods.GetOSProductName;
                        if (osEdition == string.Empty) osEdition = "(Unknown)";
                    }
                }
                return osEdition;
            }
        }

        //获取系统平台类型
        public static string OSFamily
        {
            get
            {
                string osTypeInfo = OSTypeInfo.OSPlatformType;
                if (osTypeInfo == string.Empty) osTypeInfo = "(Unknown)";
                return osTypeInfo;
            }
        }

        //检测系统是否为桌面端 Windows
        public static bool IsDesktopEdition
        {
            get
            {
                return OSTypeInfo.IsDesktopPlatform;
            }
        }

        //获取系统架构
        public static string OSArchitecture
        {
            get
            {
                return OSTypeInfo.OSPlatformArchitecture;
            }
        }
    }
}
