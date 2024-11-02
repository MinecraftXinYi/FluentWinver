using SharpWinver.Constants;
using SharpWinver.Helpers;

namespace SharpWinver.Core;

internal static class WinInstallation
{
    public static uint InstallationDateTimeRaw
    {
        get
        {
            RegistryHelper.TryGetRegDword(UsingRegistryPaths.WinNTCurrentVersion, "InstallDate", out uint datetime);
            return datetime;
        }
    }

    public static string RegisteredOwner
    {
        get
        {
            RegistryHelper.TryGetRegString(UsingRegistryPaths.WinNTCurrentVersion, "RegisteredOwner", out string owner);
            return owner;
        }
    }

    public static string RegisteredOrganization
    {
        get
        {
            RegistryHelper.TryGetRegString(UsingRegistryPaths.WinNTCurrentVersion, "RegisteredOrganization", out string organization);
            return organization;
        }
    }
}
