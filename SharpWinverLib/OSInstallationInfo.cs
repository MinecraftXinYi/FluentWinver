using System;

namespace SharpWinver;

using Services;

public static partial class OSInstallationInfo
{
    //获取系统安装日期及时间
    public static DateTime OSInstallationDateTime
    {
        get
        {
            uint datetimeSeconds = InstallationInfo.OSInstallationDateTimeRaw;
            DateTime installDateTime = DateTimeOffset.FromUnixTimeSeconds(datetimeSeconds).LocalDateTime;
            return installDateTime;
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
