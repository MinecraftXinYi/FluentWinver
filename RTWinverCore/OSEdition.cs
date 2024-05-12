using System;
using Windows.System.Profile;
using System.Runtime.InteropServices;

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
                string regOSProductName;
                RegistryHelper.TryGetInfoString(RegistryHelper.NTInfoKeyPath, "ProductName", out regOSProductName);
                return regOSProductName;
            }
        }

        static string NetOSDescription
        {
            get
            {
                string osDescription = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
                string osEditionID;
                RegistryHelper.TryGetInfoString(RegistryHelper.NTInfoKeyPath, "EditionID", out osEditionID);
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

        //获取系统平台类型
        public static string OSFamily
        {
            get
            {
                return AnalyticsInfo.VersionInfo.DeviceFamily;
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
