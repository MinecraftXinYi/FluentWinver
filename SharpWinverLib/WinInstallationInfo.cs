using System;

namespace SharpWinver;

using Core;

public static class WinInstallationInfo
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

    //获取系统注册的用户名称
    public static string OSRegisteredOwner
    {
        get
        {
            string registeredUser = WinInstallation.RegisteredOwner;
            if (registeredUser == string.Empty) registeredUser = "[Unknown system registered owner]";
            return registeredUser;
        }
    }

    //获取系统注册的组织名称
    public static string OSRegisteredOrganization
    {
        get
        {
            return WinInstallation.RegisteredOrganization;
        }
    }
}
