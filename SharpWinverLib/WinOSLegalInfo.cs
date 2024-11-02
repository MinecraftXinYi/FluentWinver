namespace SharpWinver;

using Core;

public static class WinOSLegalInfo
{
    internal const string CopyrightMicrosoftString = "(c) Microsoft Corporation.";

    //获取系统CopyRight信息
    public static string OSCopyrightString
    {
        get
        {
            if (Winbrand.CanInvoke)
            {
                return WinBrandInfo.WindowsCopyrightString;
            }
            else
            {
                return CopyrightMicrosoftString;
            }
        }
    }
}
