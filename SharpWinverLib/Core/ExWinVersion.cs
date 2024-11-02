using SharpWinver.Constants;
using SharpWinver.Helpers;

namespace SharpWinver.Core;

public static class ExWinVersion
{
    public static string WinReleaseTag
    {
        get
        {
            if (!RegistryHelper.TryGetRegString(UsingRegistryPaths.WinNTCurrentVersion, "DisplayVersion", out string version))
                if (!RegistryHelper.TryGetRegString(UsingRegistryPaths.WinNTCurrentVersion, "ReleaseId", out version))
                    RegistryHelper.TryGetRegString(UsingRegistryPaths.WinNTCurrentVersion, "CSDVersion", out version);
            return version;
        }
    }

    public static uint WinUBR
    {
        get
        {
            RegistryHelper.TryGetRegDword(UsingRegistryPaths.WinNTCurrentVersion, "UBR", out uint ubr);
            return ubr;
        }
    }
}
