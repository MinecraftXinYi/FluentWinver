using SharpWinNTInterop.Toolsets;

namespace SharpWinver.Core;

internal static class WinInstallation
{
    public static uint? InstallationDateTimeRaw
    {
        get => NTRegistryValueReader.GetDwordValue(UsingRegistryPaths.WinNTCurrentVersion, "InstallDate");
    }

    public static string? RegisteredOwner
    {
        get => NTRegistryValueReader.GetStringValue(UsingRegistryPaths.WinNTCurrentVersion, "RegisteredOwner");
    }

    public static string? RegisteredOrganization
    {
        get => NTRegistryValueReader.GetStringValue(UsingRegistryPaths.WinNTCurrentVersion, "RegisteredOrganization");
    }
}
