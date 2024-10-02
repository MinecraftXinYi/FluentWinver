using SharpWinver.Helpers;

namespace SharpWinver.Core;

public static class ExWinVersion
{
    static ExWinVersion()
    {
        if (!UsingRegistryKeys.AllLoaded) UsingRegistryKeys.Load();
    }

    public static string WinReleaseTag
    {
        get
        {
            if (!RegistryHelper.TryGetRegString(UsingRegistryKeys.WinNTCurrent, "DisplayVersion", out string version))
                if (!RegistryHelper.TryGetRegString(UsingRegistryKeys.WinNTCurrent, "ReleaseId", out version))
                    RegistryHelper.TryGetRegString(UsingRegistryKeys.WinNTCurrent, "CSDVersion", out version);
            return version;
        }
    }

    public static uint WinUBR
    {
        get
        {
            RegistryHelper.TryGetRegDword(UsingRegistryKeys.WinNTCurrent, "UBR", out uint ubr);
            return ubr;
        }
    }
}
