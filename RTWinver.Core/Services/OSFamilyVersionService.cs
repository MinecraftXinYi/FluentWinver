using Windows.System.Profile;

namespace RTWinver.Services;

internal static class OSFamilyVersionService
{
    static string OSFamilyVersionRaw
    {
        get => AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
    }

    static ulong OSFamilyVersionRawUlong
    {
        get
        {
            ulong osFamilyVersionUlong = 0000000000000000;
            ulong.TryParse(OSFamilyVersionRaw, out osFamilyVersionUlong);
            return osFamilyVersionUlong;
        }
    }

    public static ulong OSFamilyVersionMajor
    {
        get
        {
            return (OSFamilyVersionRawUlong & 0xFFFF000000000000L) >> 48;
        }
    }

    public static ulong OSFamilyVersionMinor
    {
        get
        {
            return (OSFamilyVersionRawUlong & 0x0000FFFF00000000L) >> 32;
        }
    }

    public static ulong OSFamilyVersionRevision
    {
        get
        {
            return (OSFamilyVersionRawUlong & 0x00000000FFFF0000L) >> 16;
        }
    }

    public static ulong OSFamilyVersionBuild
    {
        get
        {
            return (OSFamilyVersionRawUlong & 0x000000000000FFFFL);
        }
    }

}
