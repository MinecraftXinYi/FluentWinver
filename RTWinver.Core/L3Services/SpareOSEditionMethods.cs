﻿using RTWinver.Helpers;
using RTWinver.L2Services;

namespace RTWinver.L3Services;

internal static class SpareOSEditionMethods
{
    static string GuessOSNumber
    {
        get
        {
            string osNumber = string.Empty;
            if (OSVersionRaw.OSVersionMajor == 10)
            {
                if (OSVersionRaw.OSVersionMinor == 0)
                {
                    if (OSVersionRaw.OSVersionRevision <= 21390) osNumber = "10";
                    if (OSVersionRaw.OSVersionRevision >= 21996) osNumber = "11";
                }
            }
            return osNumber;
        }
    }

    public static string CreateDesktopEditionName
    {
        get
        {
            RegistryHelper.TryGetInfoString(RegistryKeyPaths.NTInfoKeyPath, "EditionID", out string osEditionID);
            string string2 = string.Empty;
            string string3 = " Desktop";
            if (GuessOSNumber != string.Empty) string2 = $" {GuessOSNumber}";
            if (osEditionID != string.Empty) string3 = $" {osEditionID}";
            return "Windows" + string2 + string3;
        }
    }

    public static string GetOSProductName
    {
        get
        {
            RegistryHelper.TryGetInfoString(RegistryKeyPaths.NTInfoKeyPath, "ProductName", out string getOSProductName);
            return getOSProductName;
        }
    }
}
