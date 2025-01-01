using NanoWin32Registry;
using SharpWinver.Constants;
using System.Runtime.InteropServices;

namespace SharpWinver.Core;

public static class WinNTEdition
{
    public static string? ProductName
    {
        get
        {
            NanoRegistryManager.TryGetStringValue(UsingRegistryPaths.WinNTCurrentVersion, "ProductName", out string? product);
            return product;
        }
    }

    public static string? EditionID
    {
        get
        {
            NanoRegistryManager.TryGetStringValue(UsingRegistryPaths.WinNTCurrentVersion, "EditionID", out string? edition);
            return edition;
        }
    }

    public static string PlatformArchitecture
    {
        get => RuntimeInformation.OSArchitecture.ToString();
    }
}
