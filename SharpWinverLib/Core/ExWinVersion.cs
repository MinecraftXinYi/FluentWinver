using NanoWin32Registry;
using SharpWinver.Constants;

namespace SharpWinver.Core;

public static class ExWinVersion
{
    public static string? WinReleaseTag
    {
        get
        {
            if (!NanoRegistryManager.TryGetStringValue(UsingRegistryPaths.WinNTCurrentVersion, "DisplayVersion", out string? version))
                if (!NanoRegistryManager.TryGetStringValue(UsingRegistryPaths.WinNTCurrentVersion, "ReleaseId", out version))
                    NanoRegistryManager.TryGetStringValue(UsingRegistryPaths.WinNTCurrentVersion, "CSDVersion", out version);
            return version;
        }
    }

    public static uint? WinUBR
    {
        get
        {
            NanoRegistryManager.TryGetDwordValue(UsingRegistryPaths.WinNTCurrentVersion, "UBR", out uint? ubr);
            return ubr;
        }
    }
}
