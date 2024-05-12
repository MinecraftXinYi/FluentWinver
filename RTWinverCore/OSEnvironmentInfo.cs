using System;

namespace RTWinver
{
    using Services;

    public static partial class OSEnvironmentInfo
    {
        //获取系统安装时间
        public static DateTime OSInstallationDate
        {
            get
            {
                uint seconds;
                RegistryHelper.TryGetInfoDword(RegistryHelper.NTInfoKeyPath, "InstallDate", out seconds);
                DateTime installDate = DateTimeOffset.FromUnixTimeSeconds(seconds).LocalDateTime;
                return installDate;
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
