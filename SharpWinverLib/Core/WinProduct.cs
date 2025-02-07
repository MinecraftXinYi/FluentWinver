using SharpWinNTInterop.Toolsets;
using System;
using System.Runtime.InteropServices;

namespace SharpWinver.Core;

public static class WinProduct
{
    public static string? GetWindowsProductName()
        => NTRegistryValueReader.GetStringValue(UsingRegistryPaths.WinNTCurrentVersion, "ProductName");

    public static bool GetProductInfoManaged(uint dwOSMajorVersion, uint dwOSMinorVersion, uint dwSpMajorVersion, uint dwSpMinorVersion, out uint dwReturnedProductType)
    {
        try
        {
            [DllImport(ExternDllName.KernelBase, SetLastError = true)]
            static extern bool GetProductInfo(uint dwOSMajorVersion, uint dwOSMinorVersion, uint dwSpMajorVersion, uint dwSpMinorVersion, out uint dwReturnedProductType);
            bool result = GetProductInfo(dwOSMajorVersion, dwOSMinorVersion, dwSpMajorVersion, dwSpMinorVersion, out dwReturnedProductType);
            return result;
        }
        catch (Exception) { }
        try
        {
            [DllImport(ExternDllName.Kernel32, SetLastError = true)]
            static extern bool GetProductInfo(uint dwOSMajorVersion, uint dwOSMinorVersion, uint dwSpMajorVersion, uint dwSpMinorVersion, out uint dwReturnedProductType);
            bool result = GetProductInfo(dwOSMajorVersion, dwOSMinorVersion, dwSpMajorVersion, dwSpMinorVersion, out dwReturnedProductType);
            return result;
        }
        catch (Exception) { }
        try
        {
            [DllImport(ExternDllName.Kernel32Legacy, SetLastError = true)]
            static extern bool GetProductInfo(uint dwOSMajorVersion, uint dwOSMinorVersion, uint dwSpMajorVersion, uint dwSpMinorVersion, out uint dwReturnedProductType);
            bool result = GetProductInfo(dwOSMajorVersion, dwOSMinorVersion, dwSpMajorVersion, dwSpMinorVersion, out dwReturnedProductType);
            return result;
        }
        catch (Exception)
        {
            dwReturnedProductType = 0;
            return false;
        }
    }

    public static WindowsSKU? GetWindowsSKUFromWinApi(uint dwOSMajorVersion, uint dwOSMinorVersion)
    {
        if (!GetProductInfoManaged(dwOSMajorVersion, dwOSMinorVersion, ConstantInts.MinVersionNumber, ConstantInts.MinVersionNumber, out uint productType)) return null;
        if (WindowsSKU.TryParse(productType.ToString(), out WindowsSKU windowsSku)) return windowsSku;
        else return null;
    }

    public static WindowsSKU? GetWindowsSKUFromRegistry()
    {
        string? editionID = NTRegistryValueReader.GetStringValue(UsingRegistryPaths.WinNTCurrentVersion, "EditionID");
        if (editionID == null) return null;
        if (WindowsSKU.TryParse(editionID, out WindowsSKU windowsSku)) return windowsSku;
        else return null;
    }
}
