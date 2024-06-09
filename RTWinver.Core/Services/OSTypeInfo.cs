using System;
using System.Runtime.InteropServices;
using Windows.System.Profile;

namespace RTWinver.Services;

internal static class OSTypeInfo
{
    public static string OSPlatformType
    {
        get
        {
            try
            {
                return AnalyticsInfo.VersionInfo.DeviceFamily;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }

    public static bool IsDesktopPlatform
    {
        get
        {
            try
            {
                return AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Desktop";
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public static string OSPlatformArchitecture
    {
        get => RuntimeInformation.OSArchitecture.ToString();
    }
}
