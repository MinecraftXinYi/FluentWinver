using System;
using System.Runtime.InteropServices;

namespace RTWinver
{
    using Services;

    public static partial class OSEnvironmentInfo
    {
        //获取系统架构
        public static string OSArchitecture
        {
            get
            {
                return RuntimeInformation.OSArchitecture.ToString();
            }
        }

        //获取系统开发者名称
        public static string OSManufacturer
        {
            get
            {
                string osManufacturer;
                try
                {
                    string ntfile = $"{Environment.SystemDirectory}/ntoskrnl.exe";
                    System.Diagnostics.FileVersionInfo ntfileInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(ntfile);
                    if (!string.IsNullOrEmpty(ntfileInfo.CompanyName))
                    {
                        osManufacturer = ntfileInfo.CompanyName;
                    }
                    else
                    {
                        osManufacturer = "Microsoft Corporation";
                    }
                }
                catch (Exception)
                {
                    osManufacturer = "Microsoft Corporation";
                }
                return osManufacturer;
            }
        }

        //获取系统安装时间
        public static DateTime OSInstallationDate
        {
            get
            {
                var secondsRaw = RegistryHelper.GetInfoDWord("InstallDate");
                uint seconds = 00000000;
                if (secondsRaw.HasValue) seconds = (uint)secondsRaw.Value;
                DateTime installDate = DateTimeOffset.FromUnixTimeSeconds(seconds).LocalDateTime;
                return installDate;
            }
        }

        //获取系统注册用户名称
        public static string OSRegisteredUser
        {
            get
            {
                string registeredUser;
                registeredUser = RegistryHelper.GetInfoString("RegisteredOwner") ?? "";
                if (string.IsNullOrEmpty(registeredUser))
                    registeredUser = "[Unknown]";
                return registeredUser;
            }
        }

        //获取系统目录路径
        public static string OSDirectory
        {
            get
            {
                return System.Environment.SystemDirectory;
            }
        }
    }
}
