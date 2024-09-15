using SharpWinver.Constants;
using SharpWinver.Helpers;

namespace SharpWinver.Core;

public static class ExWinVersion
{
    public static string WinReleaseTag
    {
        get
        {
            if (!RegistryHelper.TryGetRegString(NativeRegPaths.WinNTCurrent, "DisplayVersion", out string version))
                if (!RegistryHelper.TryGetRegString(NativeRegPaths.WinNTCurrent, "ReleaseId", out version))
                    RegistryHelper.TryGetRegString(NativeRegPaths.WinNTCurrent, "CSDVersion", out version);
            return version;
        }
    }

    public static uint WinUBR
    {
        get
        {
            RegistryHelper.TryGetRegDword(NativeRegPaths.WinNTCurrent, "UBR", out uint ubr);
            return ubr;
        }
    }
}
