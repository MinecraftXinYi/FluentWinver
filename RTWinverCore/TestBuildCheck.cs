using System;

namespace RTWinver
{
    using Services;

    public static partial class TestBuildCheck
    {
        //检测系统是否有过期时间
        public static bool HasExpirationTime
        {
            get => ExpirationInfoService.GetSystemExpiration().HasValue;
        }

        //获取测试副本过期时间
        public static DateTime? OSExpirationTime
        {
            get
            {
                return ExpirationInfoService.GetSystemExpiration();
            }
        }


    }
}
