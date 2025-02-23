using SharpWinNTInterop;
using SharpWinNTInterop.Toolsets;
using System;
using System.Runtime.InteropServices;

namespace SharpWinver.Core;

public static class WinProduct
{
    public static string? GetWindowsProductName()
        => NTRegistryValueReader.GetStringValue(ConstRegistryPaths.WinNTCurrentVersion, "ProductName");

    public static BOOLEAN GetProductInfoManaged(uint dwOSMajorVersion, uint dwOSMinorVersion, uint dwSpMajorVersion, uint dwSpMinorVersion, out uint dwReturnedProductType)
    {
        try
        {
            [DllImport(ExternDllName.KernelBase, SetLastError = true)]
            static extern BOOLEAN GetProductInfo(uint dwOSMajorVersion, uint dwOSMinorVersion, uint dwSpMajorVersion, uint dwSpMinorVersion, out uint dwReturnedProductType);
            BOOLEAN result = GetProductInfo(dwOSMajorVersion, dwOSMinorVersion, dwSpMajorVersion, dwSpMinorVersion, out dwReturnedProductType);
            return result;
        }
        catch (Exception) { }
        try
        {
            [DllImport(ExternDllName.Kernel32, SetLastError = true)]
            static extern BOOLEAN GetProductInfo(uint dwOSMajorVersion, uint dwOSMinorVersion, uint dwSpMajorVersion, uint dwSpMinorVersion, out uint dwReturnedProductType);
            BOOLEAN result = GetProductInfo(dwOSMajorVersion, dwOSMinorVersion, dwSpMajorVersion, dwSpMinorVersion, out dwReturnedProductType);
            return result;
        }
        catch (Exception) { }
        try
        {
            [DllImport(ExternDllName.Kernel32Legacy, SetLastError = true)]
            static extern BOOLEAN GetProductInfo(uint dwOSMajorVersion, uint dwOSMinorVersion, uint dwSpMajorVersion, uint dwSpMinorVersion, out uint dwReturnedProductType);
            BOOLEAN result = GetProductInfo(dwOSMajorVersion, dwOSMinorVersion, dwSpMajorVersion, dwSpMinorVersion, out dwReturnedProductType);
            return result;
        }
        catch (Exception)
        {
            dwReturnedProductType = 0;
            return BOOLEAN.FALSE;
        }
    }

    public static WindowsSKU? GetWindowsSKUFromWinApi(uint dwOSMajorVersion, uint dwOSMinorVersion)
    {
        if (!GetProductInfoManaged(dwOSMajorVersion, dwOSMinorVersion, 0, 0, out uint productType)) return null;
        if (WindowsSKU.TryParse(productType.ToString(), out WindowsSKU windowsSku)) return windowsSku;
        else return null;
    }

    public static WindowsSKU? GetWindowsSKUFromRegistry()
    {
        string? editionID = NTRegistryValueReader.GetStringValue(ConstRegistryPaths.WinNTCurrentVersion, "EditionID");
        if (editionID == null) return null;
        if (WindowsSKU.TryParse(editionID, out WindowsSKU windowsSku)) return windowsSku;
        else return null;
    }
}
