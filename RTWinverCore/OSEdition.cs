using System;
using Windows.System.Profile;

namespace RTWinver
{
    using Services;

    public static partial class OSEdition
    {
        //获取系统名称
        static string RegOSProductName
        {
            get
            {
                return RegistryHelper.GetInfoString("ProductName") ?? "";
            }
        }

        static string NetOSDescription
        {
            get
            {
                string osDescription = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
                string osEditionID = RegistryHelper.GetInfoString("EditionID") ?? "";
                string fullDescription = osDescription + " " + osEditionID;
                return fullDescription;
            }
        }

        public static string OSEditionName
        {
            get
            {
                string osEdition = "Microsoft Windows";
                try
                {
                    osEdition = WinbrandService.GetWinbrand();
                }
                catch (Exception)
                {
                    try
                    {
                        osEdition = RegOSProductName;
                    }
                    catch (Exception)
                    {
                        osEdition = NetOSDescription;
                    }
                }
                return osEdition;
            }
        }

        //获取系统类型
        public static string OSFamily
        {
            get
            {
                return AnalyticsInfo.VersionInfo.DeviceFamily;
            }
        }
    }
}
