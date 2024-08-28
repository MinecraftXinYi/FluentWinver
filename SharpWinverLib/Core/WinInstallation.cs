using SharpWinver.Helpers;
using SharpWinver.Models.Constants;

namespace SharpWinver.Core;

internal static class WinInstallation
{
    public static uint InstallationDateTimeRaw
    {
        get
        {
            RegistryHelper.TryGetRegDword(RegistryPaths.WinNTCurrent, "InstallDate", out uint datetime);
            return datetime;
        }
    }

    public static string RegisteredOwner
    {
        get
        {
            RegistryHelper.TryGetRegString(RegistryPaths.WinNTCurrent, "RegisteredOwner", out string owner);
            return owner;
        }
    }

    public static string RegisteredOrganization
    {
        get
        {
            RegistryHelper.TryGetRegString(RegistryPaths.WinNTCurrent, "RegisteredOrganization", out string organization);
            return organization;
        }
    }
}
