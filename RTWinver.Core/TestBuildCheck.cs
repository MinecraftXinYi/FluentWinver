using System;

namespace RTWinver
{
    using L2Services;

    public static partial class TestBuildCheck
    {
        //检测系统是否有过期时间
        public static bool HasExpirationTime
        {
            get
            {
                return OSExpirationRaw.HasExpiration;
            }
        }

        //获取测试副本过期时间
        public static DateTime OSExpirationTime
        {
            get
            {
                DateTime? expirationTime = OSExpirationRaw.ExpirationTime;
                if (expirationTime.HasValue) return expirationTime.Value;
                else return DateTimeOffset.FromUnixTimeSeconds(0).LocalDateTime;
            }
        }
    }
}
