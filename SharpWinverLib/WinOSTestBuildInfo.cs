using System;

namespace SharpWinver;

using Core;

public static partial class Winver
{
    /// <summary>
    /// 当前运行的 Windows 系统副本的测试构建信息
    /// </summary>
    public static class WinOSTestBuildInfo
    {
        /// <summary>
        /// 检测当前系统副本是否有过期时间
        /// </summary>
        public static bool HasExpirationTime
        {
            get
            {
                return WinExpiration.GetSystemExpiration().HasValue;
            }
        }

        /// <summary>
        /// 当前系统副本过期时间
        /// </summary>
        public static DateTime? OSExpirationTime
        {
            get
            {
                DateTime? expirationDateTime = WinExpiration.GetSystemExpiration();
                if (expirationDateTime.HasValue) return expirationDateTime.Value.ToUniversalTime();
                else return null;
            }
        }
    }
}
