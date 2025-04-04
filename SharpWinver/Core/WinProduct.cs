using MinecraftXinYi.WindowsNT;
using System;
using System.Runtime.InteropServices;

namespace SharpWinver.Core;

public static class WinProduct
{
    public static string? GetWindowsProductName()
        => NTRegistryValueReader.GetStringValue(ConstRegistryPaths.WinNTCurrentVersion, "ProductName");

    public static bool TryGetProductInfo(uint dwOSMajorVersion, uint dwOSMinorVersion, uint dwSpMajorVersion, uint dwSpMinorVersion, out uint dwReturnedProductType)
    {
        try
        {
            [DllImport(DllName.KernelBase, SetLastError = true)]
            [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
            static extern BOOL GetProductInfo(uint dwOSMajorVersion, uint dwOSMinorVersion, uint dwSpMajorVersion, uint dwSpMinorVersion, out uint dwReturnedProductType);
            BOOL result = GetProductInfo(dwOSMajorVersion, dwOSMinorVersion, dwSpMajorVersion, dwSpMinorVersion, out dwReturnedProductType);
            return result;
        }
        catch (Exception)
        {
            try
            {
                [DllImport(DllName.Kernel32, SetLastError = true)]
                [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
                static extern BOOL GetProductInfo(uint dwOSMajorVersion, uint dwOSMinorVersion, uint dwSpMajorVersion, uint dwSpMinorVersion, out uint dwReturnedProductType);
                BOOL result = GetProductInfo(dwOSMajorVersion, dwOSMinorVersion, dwSpMajorVersion, dwSpMinorVersion, out dwReturnedProductType);
                return result;
            }
            catch (Exception)
            {
                dwReturnedProductType = ((uint?)null).GetValueOrDefault();
                return false;
            }
        }
    }

    public static WindowsSKU GetWindowsSKUFromWinApi(uint dwOSMajorVersion, uint dwOSMinorVersion)
    {
        if (!TryGetProductInfo(dwOSMajorVersion, dwOSMinorVersion, 0, 0, out uint productType)) return WindowsSKU.Undefined;
        if (!WindowsSKU.TryParse(productType.ToString(), out WindowsSKU windowsSku)) return WindowsSKU.Undefined;
        return windowsSku;
    }

    public static WindowsSKU GetWindowsSKUFromRegistry()
    {
        string editionID = NTRegistryValueReader.GetStringValue(ConstRegistryPaths.WinNTCurrentVersion, "EditionID")!;
        if (string.IsNullOrEmpty(editionID)) return WindowsSKU.Undefined;
        if (!WindowsSKU.TryParse(editionID, out WindowsSKU windowsSku)) return WindowsSKU.Undefined;
        return windowsSku;
    }
}
