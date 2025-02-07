using System.Runtime.InteropServices;

namespace SharpWinver;

using Core;

public static partial class Winver
{
    /// <summary>
    /// 当前运行的 Windows 系统 SKU 版本
    /// </summary>
    public static class WindowsEdition
    {
        /// <summary>
        /// Internal method
        /// </summary>
        /// <returns></returns>
        internal static WindowsSKU GetWindowsSKUSafe()
        {
            uint[] ntVersion = WinNTVersion.GetNtVersion();
            WindowsSKU? windowsSku = WinProduct.GetWindowsSKUFromWinApi(ntVersion[0], ntVersion[1]);
            windowsSku ??= WinProduct.GetWindowsSKUFromRegistry();
            windowsSku ??= WindowsSKU.Undefined;
            return windowsSku.Value;
        }

        /// <summary>
        /// Windows 系统 SKU 版本名称
        /// </summary>
        public static string WindowsEditionName
        {
            get
            {
                string osEdition = string.Empty;
                if (WinBrand.CanInvoke) osEdition = WinBrand.BrandingFormatString(WinBrand.VariableNames.WindowsLong);
                if (string.IsNullOrEmpty(osEdition)) osEdition = WinProduct.GetWindowsProductName() ?? string.Empty;
                if (string.IsNullOrEmpty(osEdition)) osEdition = $"{ConstantStrings.WindowsGeneric} {GetWindowsSKUSafe()}";
                return osEdition;
            }
        }

        /// <summary>
        /// Windows 系统 SKU 名称
        /// </summary>
        public static string WindowsSKUName
        {
            get
            {
                return GetWindowsSKUSafe().ToString();
            }
        }

        /// <summary>
        /// Windows 系统 SKU ID
        /// </summary>
        public static uint WindowsSKUID
        {
            get
            {
                return (uint)GetWindowsSKUSafe();
            }
        }
    }

    /// <summary>
    /// 当前运行的 Windows 系统平台架构
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
