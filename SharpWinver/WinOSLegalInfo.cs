namespace SharpWinver;

using Core;

public static partial class Winver
{
    /// <summary>
    /// 当前运行的 Windows 系统副本的版权条款信息
    /// </summary>
    public static class WinOSLegalInfo
    {
        /// <summary>
        /// 当前系统副本的版权声明信息
        /// </summary>
        public static string OSCopyrightString
        {
            get
            {
                if (WinBrand.CanInvoke) return WinBrand.BrandingFormatString(WinBrand.VariableNames.WindowsCopyright);
                else return ConstantStrings.DefaultMicrosoftCopyrightString;
            }
        }
    }
}
