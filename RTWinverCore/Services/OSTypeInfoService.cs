using Windows.System.Profile;

namespace RTWinver.Services;

internal static class OSTypeInfoService
{
    public static string OSPlatformType
    {
        get
        {
            return AnalyticsInfo.VersionInfo.DeviceFamily;
        }
    }

    public static bool IsDesktopPlatform
    {
        get => AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Desktop";
    }
}
