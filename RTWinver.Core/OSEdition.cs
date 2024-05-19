using System;
using System.Runtime.InteropServices;

namespace RTWinver
{
    using Services;

    public static partial class OSEdition
    {
        //获取系统类型名称
        public static string OSEditionName
        {
            get
            {
                string osEdition = "Windows";
                try
                {
                    osEdition = WinbrandService.WindowsLong;
                }
                catch (Exception)
                {
                    if (OSTypeInfoService.IsDesktopPlatform)
                    {
                        osEdition = SpareOSEditionMethods.CreateDesktopEditionName;
                    }
                    else
                    {
                        osEdition = SpareOSEditionMethods.GetOSProductName;
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
                return OSTypeInfoService.OSPlatformType;
            }
        }

        //检测系统是否为桌面端 Windows
        public static bool IsDesktopEdition
        {
            get
            {
                return OSTypeInfoService.IsDesktopPlatform;
            }
        }

        //获取系统架构
        public static string OSArchitecture
        {
            get
            {
                return RuntimeInformation.OSArchitecture.ToString();
            }
        }
    }
}
