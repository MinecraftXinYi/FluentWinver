using System;

namespace SharpWinver;

using Core;
using Constants;

public static partial class Winver
{
    public static class WinInstallationInfo
    {
        //获取系统安装日期及时间
        public static DateTime OSInstallationDateTime
        {
            get
            {
                uint? datetimeSeconds = WinInstallation.InstallationDateTimeRaw;
                UnixDateTime.TryConvertFromUnixTimeSeconds(datetimeSeconds ?? 0, out DateTime? installDateTime);
                return installDateTime ?? UnixDateTime.MinValue;
            }
        }

        //获取系统注册的用户名称
        public static string OSRegisteredOwner
        {
            get
            {
                string? registeredUser = WinInstallation.RegisteredOwner;
                if (registeredUser == null || registeredUser == string.Empty)
                    registeredUser = ConstantStrings.IUnknown;
                return registeredUser;
            }
        }

        //获取系统注册的组织名称
        public static string OSRegisteredOrganization
        {
            get
            {
                string? registeredOrganization = WinInstallation.RegisteredOrganization;
                registeredOrganization ??= ConstantStrings.IUnknown;
                return registeredOrganization;
            }
        }
    }
}
