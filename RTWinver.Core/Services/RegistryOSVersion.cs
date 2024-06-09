using RTWinver.Helpers;

namespace RTWinver.Services;

internal static class RegistryOSVersion
{
    public static ulong OSVersionMajor
    {
        get
        {
            RegistryHelper.TryGetInfoDword(RegistryKeyPaths.NTInfoKeyPath, "CurrentMajorVersionNumber", out uint majorNumber);
            return majorNumber;
        }
    }

    public static ulong OSVersionMinor
    {
        get
        {
            RegistryHelper.TryGetInfoDword(RegistryKeyPaths.NTInfoKeyPath, "CurrentMinorVersionNumber", out uint minorNumber);
            return minorNumber;
        }
    }

    public static ulong OSVersionRevision
    {
        get
        {
            RegistryHelper.TryGetInfoString(RegistryKeyPaths.NTInfoKeyPath, "CurrentBuildNumber", out string rawData);
            if (ulong.TryParse(rawData, out ulong revisionNumber)) return revisionNumber;
            else return 0;
        }
    }

    public static ulong OSVersionBuild
    {
        get
        {
            RegistryHelper.TryGetInfoDword(RegistryKeyPaths.NTInfoKeyPath, "UBR", out uint ubuildNumber);
            return ubuildNumber;
        }
    }
}
