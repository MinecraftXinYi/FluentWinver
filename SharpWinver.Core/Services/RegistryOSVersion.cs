using SharpWinver.Helpers;

namespace SharpWinver.Services;

internal static class RegistryOSVersion
{
    public static ulong OSVersionMajor
    {
        get
        {
            RegistryHelper.TryGetRegDword(RegistryPaths.WinNTCurrent, "CurrentMajorVersionNumber", out uint majorNumber);
            return majorNumber;
        }
    }

    public static ulong OSVersionMinor
    {
        get
        {
            RegistryHelper.TryGetRegDword(RegistryPaths.WinNTCurrent, "CurrentMinorVersionNumber", out uint minorNumber);
            return minorNumber;
        }
    }

    public static ulong OSVersionRevision
    {
        get
        {
            RegistryHelper.TryGetRegString(RegistryPaths.WinNTCurrent, "CurrentBuildNumber", out string rawData);
            if (ulong.TryParse(rawData, out ulong revisionNumber)) return revisionNumber;
            else return 0;
        }
    }

    public static ulong OSVersionBuild
    {
        get
        {
            RegistryHelper.TryGetRegDword(RegistryPaths.WinNTCurrent, "UBR", out uint ubuildNumber);
            return ubuildNumber;
        }
    }
}
