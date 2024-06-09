using System;

namespace RTWinver
{
    using Services;

    public static partial class OSEnvironmentInfo
    {
        //获取系统安装日期时间
        public static DateTime OSInstallationDateTime
        {
            get
            {
                uint dateSeconds = InstallationInfo.OSInstallationDateTimeRaw;
                DateTime installDate = DateTimeOffset.FromUnixTimeSeconds(dateSeconds).LocalDateTime;
                return installDate;
            }
        }

        //获取系统目录路径
        public static string OSDirectory
        {
            get
            {
                return InstallationInfo.OSDirectoryPath;
            }
        }
    }
}
