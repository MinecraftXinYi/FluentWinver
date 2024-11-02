using SharpWinver.Constants;
using SharpWinver.Helpers;
using System.Runtime.InteropServices;

namespace SharpWinver.Core;

public static class WinNTEdition
{
    public static string ProductName
    {
        get
        {
            RegistryHelper.TryGetRegString(UsingRegistryPaths.WinNTCurrentVersion, "ProductName", out string product);
            return product;
        }
    }

    public static string EditionID
    {
        get
        {
            RegistryHelper.TryGetRegString(UsingRegistryPaths.WinNTCurrentVersion, "EditionID", out string edition);
            return edition;
        }
    }

    public static string PlatformArchitecture
    {
        get => RuntimeInformation.OSArchitecture.ToString();
    }
}
