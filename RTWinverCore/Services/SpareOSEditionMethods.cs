namespace RTWinver.Services;

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
            string baseName = "Windows";
            string osEditionID;
            RegistryHelper.TryGetInfoString(RegistryKeyPaths.NTInfoKeyPath, "EditionID", out osEditionID);
            if (string.IsNullOrEmpty(GuessOSNumber) || string.IsNullOrEmpty(osEditionID))
                baseName = "Microsoft Windows";
            string string1 = string.Empty;
            string string2 = string.Empty;
            if (!string.IsNullOrEmpty(GuessOSNumber))
                string1 = $" {GuessOSNumber}";
            if (!string.IsNullOrEmpty(osEditionID))
                string2 = $" {osEditionID}";
            return baseName + string1 + string2;
        }
    }

    public static string GetOSProductName
    {
        get
        {
            string getOSProductName;
            RegistryHelper.TryGetInfoString(RegistryKeyPaths.NTInfoKeyPath, "ProductName", out getOSProductName);
            return getOSProductName;
        }
    }
}
