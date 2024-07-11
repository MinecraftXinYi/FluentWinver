using System;

namespace SharpWinver
{
    using Services;

    public static partial class OSTestBuildCheck
    {
        //检测系统是否有过期时间
        public static bool HasExpirationTime
        {
            get
            {
                return ExpirationInfo.GetSystemExpiration().HasValue;
            }
        }

        //获取测试副本过期时间
        public static DateTime OSExpirationTime
        {
            get
            {
                DateTime? expirationTime = ExpirationInfo.GetSystemExpiration();
                if (expirationTime.HasValue) return expirationTime.Value;
                else return DateTimeOffset.FromUnixTimeSeconds(0).LocalDateTime;
            }
        }
    }
}
