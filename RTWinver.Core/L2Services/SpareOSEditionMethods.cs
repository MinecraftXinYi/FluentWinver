using RTWinver.Helpers;
using RTWinver.Services;

namespace RTWinver.L2Services;

internal static class SpareOSEditionMethods
{
    static string GuessOSNumber
    {
        get
        {
            string osNumber = string.Empty;
            if (OSFamilyVersionService.OSFamilyVersionMajor == 10)
                if (OSFamilyVersionService.OSFamilyVersionMinor == 0)
                {
                    if (OSFamilyVersionService.OSFamilyVersionRevision <= 21390)
                        osNumber = "10";
                    if (OSFamilyVersionService.OSFamilyVersionRevision >= 21996)
                        osNumber = "11";
                }
            return osNumber;
        }
    }

    public static string CreateDesktopEditionName
    {
        get
        {
            string osEditionID;
            RegistryHelper.TryGetInfoString(RegistryKeyPaths.NTInfoKeyPath, "EditionID", out osEditionID);
            string string2 = string.Empty;
            string string3 = string.Empty;
            if (!string.IsNullOrEmpty(GuessOSNumber))
                string2 = $" {GuessOSNumber}";
            if (!string.IsNullOrEmpty(osEditionID))
                string3 = $" {osEditionID}";
            if (string2 != string.Empty || string3 != string.Empty)
                return "Windows" + string2 + string3;
            else return "Windows Desktop";
        }
    }

    public static string GetOSProductName
    {
        get
        {
            string getOSProductName;
            RegistryHelper.TryGetInfoString(RegistryKeyPaths.NTInfoKeyPath, "ProductName", out getOSProductName);
            if (getOSProductName == string.Empty)
                getOSProductName = OSTypeInfoService.OSPlatformType;
            return getOSProductName;
        }
    }
}
