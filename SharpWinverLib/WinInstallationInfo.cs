using System;

namespace SharpWinver;

using Core;

public class WinInstallationInfo
{
    //获取系统安装日期及时间
    public static DateTime OSInstallationDateTime
    {
        get
        {
            uint datetimeSeconds = WinInstallation.InstallationDateTimeRaw;
            DateTime installDateTime = DateTimeOffset.FromUnixTimeSeconds(datetimeSeconds).LocalDateTime;
            return installDateTime;
        }
    }

    public static string OSDirectory
    {
        get
        {
            return Environment.SystemDirectory;
        }
    }
}
