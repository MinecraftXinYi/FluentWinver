using NanoWin32Registry;
using SharpWinver.Constants;

namespace SharpWinver.Core;

internal static class WinInstallation
{
    public static uint? InstallationDateTimeRaw
    {
        get
        {
            NanoRegistryManager.TryGetDwordValue(UsingRegistryPaths.WinNTCurrentVersion, "InstallDate", out uint? datetime);
            return datetime;
        }
    }

    public static string? RegisteredOwner
    {
        get
        {
            NanoRegistryManager.TryGetStringValue(UsingRegistryPaths.WinNTCurrentVersion, "RegisteredOwner", out string? owner);
            return owner;
        }
    }

    public static string? RegisteredOrganization
    {
        get
        {
            NanoRegistryManager.TryGetStringValue(UsingRegistryPaths.WinNTCurrentVersion, "RegisteredOrganization", out string? organization);
            return organization;
        }
    }
}
