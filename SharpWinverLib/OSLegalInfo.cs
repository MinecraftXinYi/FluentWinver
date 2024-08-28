namespace SharpWinver;

using Core;

public class OSLegalInfo
{
    internal const string CopyrightMicrosoftString = "(c) Microsoft Corporation.";

    //获取系统CopyRight信息
    public static string OSCopyrightString
    {
        get
        {
            if (Winbrand.CanInvoke)
            {
                return Winbrand.WinBrandInfo.WindowsCopyright;
            }
            else
            {
                return CopyrightMicrosoftString;
            }
        }
    }

    //获取系统注册用户名称
    public static string OSRegisteredOwner
    {
        get
        {
            string registeredUser = WinInstallation.RegisteredOwner;
            if (registeredUser == string.Empty) registeredUser = "[Unknown system registered owner]";
            return registeredUser;
        }
    }

    public static string OSRegisteredOrganization
    {
        get
        {
            return WinInstallation.RegisteredOrganization;
        }
    }
}
