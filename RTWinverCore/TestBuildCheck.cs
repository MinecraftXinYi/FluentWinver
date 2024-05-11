using System;

namespace RTWinver
{
    using Services;

    public static partial class TestBuildCheck
    {
        //检测是否为限时测试版系统
        public static bool IsTestBuild
        {
            get
            {
                DateTime? expiration = ExpirationInfoService.GetSystemExpiration();
                if (expiration != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
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
