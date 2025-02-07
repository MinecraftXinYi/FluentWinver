using System;

namespace SharpWinver;

using Core;

public static partial class Winver
{
    /// <summary>
    /// 当前运行的 Windows 系统副本的安装信息
    /// </summary>
    public static class WinInstallationInfo
    {
        /// <summary>
        /// 当前系统副本的安装日期及时间
        /// </summary>
        public static DateTime OSInstallationDateTime
        {
            get
            {
                uint? datetimeSeconds = WinInstallation.InstallationDateTimeRaw;
                UnixDateTime.TryConvertFromUnixTimeSeconds(datetimeSeconds ?? 0, out DateTime? installDateTime);
                return installDateTime ?? UnixDateTime.MinValue;
            }
        }

        /// <summary>
        /// 注册当前系统副本的用户名称
        /// </summary>
        public static string OSRegisteredOwner
        {
            get
            {
                string? registeredUser = WinInstallation.RegisteredOwner;
                if (registeredUser == null || registeredUser == string.Empty) registeredUser = ConstantStrings.ErrorMsg;
                return registeredUser;
            }
        }

        /// <summary>
        /// 注册当前系统副本的组织名称
        /// </summary>
        public static string OSRegisteredOrganization
        {
            get
            {
                string? registeredOrganization = WinInstallation.RegisteredOrganization;
                registeredOrganization ??= ConstantStrings.ErrorMsg;
                return registeredOrganization;
            }
        }
    }
}
