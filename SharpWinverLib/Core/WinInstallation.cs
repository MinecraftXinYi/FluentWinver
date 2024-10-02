using SharpWinver.Helpers;

namespace SharpWinver.Core;

internal static class WinInstallation
{
    static WinInstallation()
    {
        if (!UsingRegistryKeys.AllLoaded) UsingRegistryKeys.Load();
    }

    public static uint InstallationDateTimeRaw
    {
        get
        {
            RegistryHelper.TryGetRegDword(UsingRegistryKeys.WinNTCurrent, "InstallDate", out uint datetime);
            return datetime;
        }
    }

    public static string RegisteredOwner
    {
        get
        {
            RegistryHelper.TryGetRegString(UsingRegistryKeys.WinNTCurrent, "RegisteredOwner", out string owner);
            return owner;
        }
    }

    public static string RegisteredOrganization
    {
        get
        {
            RegistryHelper.TryGetRegString(UsingRegistryKeys.WinNTCurrent, "RegisteredOrganization", out string organization);
            return organization;
        }
    }
}
