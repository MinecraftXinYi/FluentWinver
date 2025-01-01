using System;

namespace SharpWinver;

using Core;

public static partial class Winver
{
    public static class WinOSTestBuildInfo
    {
        //检测系统是否有过期时间
        public static bool HasExpirationTime
        {
            get
            {
                return WinExpiration.GetSystemExpiration().HasValue;
            }
        }

        //获取测试副本过期时间
        public static DateTime? OSExpirationTime
        {
            get
            {
                return WinExpiration.GetSystemExpiration();
            }
        }
    }
}
