using SharpWinver.Helpers;

namespace SharpWinver.Services;

internal static class InstallationInfo
{
    public static uint OSInstallationDateTimeRaw
    {
        get
        {
            RegistryHelper.TryGetRegDword(RegistryPaths.WinNTCurrent, "InstallDate", out uint installDateTimeSeconds);
            return installDateTimeSeconds;
        }
    }

    public static string OSRegisteredUserRaw
    {
        get
        {
            RegistryHelper.TryGetRegString(RegistryPaths.WinNTCurrent, "RegisteredOwner", out string registeredUserString);
            return registeredUserString;
        }
    }

    public static string OSDirectoryPath
    {
        get => System.Environment.SystemDirectory;
    }
}
