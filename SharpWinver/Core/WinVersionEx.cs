namespace SharpWinver.Core;

public static class WinVersionEx
{
    public static string? GetWindowsVersionTag()
    {
        string? version = NTRegistryValueReader.GetStringValue(ConstRegistryPaths.WinNTCurrentVersion, "DisplayVersion");
        version ??= NTRegistryValueReader.GetStringValue(ConstRegistryPaths.WinNTCurrentVersion, "ReleaseId");
        version ??= NTRegistryValueReader.GetStringValue(ConstRegistryPaths.WinNTCurrentVersion, "CSDVersion");
        return version;
    }

    public static uint GetWindowsRevisionNumber()
    {
        uint? revision = NTRegistryValueReader.GetDwordValue(ConstRegistryPaths.WinNTCurrentVersion, "UBR");
        revision ??= NTRegistryValueReader.GetDwordValue(ConstRegistryPaths.WinNTCurrentVersion, "BaseBuildRevisionNumber");
        return revision.GetValueOrDefault();
    }
}
