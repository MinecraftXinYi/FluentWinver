using System.Runtime.InteropServices;

namespace SharpWinver;

using Core;

public static partial class Winver
{
    /// <summary>
    /// 当前运行的 Windows 系统 SKU
    /// </summary>
    public static class WindowsEdition
    {
        /// <summary>
        /// Windows SKU
        /// </summary>
        public static WindowsSKU SKU
        {
            get
            {
                WinNTVersion.RtlGetNtVersionNumbers(out uint major, out uint minor, out _);
                WindowsSKU windowsSku = WinProduct.GetWindowsSKUFromWinApi(major, minor);
                if (windowsSku == WindowsSKU.Undefined) windowsSku = WinProduct.GetWindowsSKUFromRegistry();
                return windowsSku;
            }
        }

        /// <summary>
        /// Windows SKU 名称
        /// </summary>
        public static string OSEdition
        {
            get
            {
                string osEdition = null!;
                if (WinBrand.CanInvoke) osEdition = WinBrand.BrandingFormatString(WinBrand.VariableNames.WindowsLong);
                if (string.IsNullOrEmpty(osEdition)) osEdition = WinProduct.GetWindowsProductName()!;
                if (string.IsNullOrEmpty(osEdition)) osEdition = $"{ConstantStrings.WindowsGeneric} {SKU}";
                return osEdition;
            }
        }

        /// <summary>
        /// 操作系统平台架构
        /// </summary>
        public static Architecture OSArchitecture
        {
            get
            {
                Architecture? architecture = WinNTProcArch.GetNTOSArchitecture2();
                architecture ??= WinNTProcArch.GetNTOSArchitecture1();
                architecture ??= RuntimeInformation.ProcessArchitecture;
                return architecture.Value;
            }
        }
    }
}
