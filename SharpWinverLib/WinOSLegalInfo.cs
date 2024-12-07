namespace SharpWinver;

using Core;
using Constants;

public static partial class Winver
{
    public static class WinOSLegalInfo
    {
        //获取系统CopyRight信息
        public static string OSCopyrightString
        {
            get
            {
                if (WinBrand.CanInvoke)
                {
                    return WinBrandInfo.WindowsCopyrightString;
                }
                else
                {
                    return ConstantStrings.CopyrightMicrosoftString;
                }
            }
        }
    }
}
