using SharpWinver.Helpers;
using SharpWinver.L2Services;
using SharpWinver.Models.Constants;

namespace SharpWinver.L3Services;

internal static class SpareOSEditionMethods
{
    static string GuessOSNumber
    {
        get
        {
            string osNumber = string.Empty;
            if (VersionInfo.OSVersionMajor == 10)
            {
                if (VersionInfo.OSVersionMinor == 0)
                {
                    if (VersionInfo.OSVersionRevision <= 21390) osNumber = "10";
                    if (VersionInfo.OSVersionRevision >= 21996) osNumber = "11";
                }
            }
            return osNumber;
        }
    }

    public static string CreateDesktopEditionName
    {
        get
        {
            RegistryHelper.TryGetRegString(RegistryPaths.WinNTCurrent, "EditionID", out string osEditionID);
            string string2 = string.Empty;
            string string3 = " Desktop";
            if (GuessOSNumber != string.Empty) string2 = $" {GuessOSNumber}";
            if (osEditionID != string.Empty) string3 = $" {osEditionID}";
            string winDesktopEdition = "Windows" + string2 + string3;
            return winDesktopEdition;
        }
    }

    public static string GetOSProductName
    {
        get
        {
            RegistryHelper.TryGetRegString(RegistryPaths.WinNTCurrent, "ProductName", out string getOSProductName);
            return getOSProductName;
        }
    }
}
