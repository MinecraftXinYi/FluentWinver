namespace SharpWinver.Core;

internal static class WinInstallation
{
    public static uint? InstallationDateTimeRaw
    {
        get => NTRegistryValueReader.GetDwordValue(ConstRegistryPaths.WinNTCurrentVersion, "InstallDate");
    }

    public static string? RegisteredOwner
    {
        get => NTRegistryValueReader.GetStringValue(ConstRegistryPaths.WinNTCurrentVersion, "RegisteredOwner");
    }

    public static string? RegisteredOrganization
    {
        get => NTRegistryValueReader.GetStringValue(ConstRegistryPaths.WinNTCurrentVersion, "RegisteredOrganization");
    }
}
