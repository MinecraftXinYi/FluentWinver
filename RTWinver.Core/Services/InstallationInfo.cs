using RTWinver.Helpers;

namespace RTWinver.Services;

internal static class InstallationInfo
{
    public static uint OSInstallationDateTimeRaw
    {
        get
        {
            RegistryHelper.TryGetInfoDword(RegistryKeyPaths.NTInfoKeyPath, "InstallDate", out uint installDateTimeSeconds);
            return installDateTimeSeconds;
        }
    }

    public static string OSRegisteredUserRaw
    {
        get
        {
            RegistryHelper.TryGetInfoString(RegistryKeyPaths.NTInfoKeyPath, "RegisteredOwner", out string registeredUserString);
            return registeredUserString;
        }
    }

    public static string OSDirectoryPath
    {
        get => System.Environment.SystemDirectory;
    }
}
