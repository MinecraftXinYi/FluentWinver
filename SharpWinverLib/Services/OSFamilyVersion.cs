using Windows.System.Profile;

namespace SharpWinver.Services;

internal static class OSFamilyVersion
{
    static string OSFamilyVersionRaw
    {
        get => AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
    }

    static ulong OSFamilyVersionRawUlong
    {
        get => ulong.Parse(OSFamilyVersionRaw);
    }

    public static ulong OSFamilyVersionMajor
    {
        get => (OSFamilyVersionRawUlong & 0xFFFF000000000000L) >> 48;
    }

    public static ulong OSFamilyVersionMinor
    {
        get => (OSFamilyVersionRawUlong & 0x0000FFFF00000000L) >> 32;
    }

    public static ulong OSFamilyVersionRevision
    {
        get => (OSFamilyVersionRawUlong & 0x00000000FFFF0000L) >> 16;
    }

    public static ulong OSFamilyVersionBuild
    {
        get => (OSFamilyVersionRawUlong & 0x000000000000FFFFL) >> 0;
    }
}
