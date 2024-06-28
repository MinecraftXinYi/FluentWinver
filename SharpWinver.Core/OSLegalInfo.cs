using System;

namespace SharpWinver
{
    using Services;

    public static partial class OSLegalInfo
    {
        static readonly string MicrosoftCompString = "(c) Microsoft Corporation";

        //获取系统CopyRight信息
        public static string OSCopyRightString
        {
            get
            {
                if (WinbrandAPI.CanInvoke)
                {
                    return WinbrandAPI.GetWinBrandInfo.WindowsCopyRight;
                }
                else
                {
                    return MicrosoftCompString;
                }
            }
        }

        //获取系统注册用户名称
        public static string OSRegisteredUser
        {
            get
            {
                string registeredUser = InstallationInfo.OSRegisteredUserRaw;
                if (registeredUser == string.Empty) registeredUser = "(Unknown registered owner)";
                return registeredUser;
            }
        }
    }
}
