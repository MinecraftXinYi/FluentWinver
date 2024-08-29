using SharpWinver.Helpers;
using SharpWinver.Models.Constants;

namespace SharpWinver.Core;

public static class ExWinVersion
{
    public static string WinReleaseTag
    {
        get
        {
            if (!RegistryHelper.TryGetRegString(RegistryPaths.WinNTCurrent, "DisplayVersion", out string version))
                if (!RegistryHelper.TryGetRegString(RegistryPaths.WinNTCurrent, "ReleaseId", out version))
                    RegistryHelper.TryGetRegString(RegistryPaths.WinNTCurrent, "CSDVersion", out version);
            return version;
        }
    }

    public static uint WinUBR
    {
        get
        {
            RegistryHelper.TryGetRegDword(RegistryPaths.WinNTCurrent, "UBR", out uint ubr);
            return ubr;
        }
    }
}
