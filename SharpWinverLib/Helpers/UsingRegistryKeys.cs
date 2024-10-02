using Microsoft.Win32;
using SharpWinver.Constants;
using System;

namespace SharpWinver.Helpers;

internal static class UsingRegistryKeys
{
    public static void Load()
    {
        try
        {
            RegistryKey hkLM = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
            WinNTCurrent = hkLM.OpenSubKey(UsingSubKeyPaths.SMWNC, false);
            SystemAppxList = hkLM.OpenSubKey(UsingSubKeyPaths.SMWCAAI, false);
            AllLoaded = WinNTCurrent != null && SystemAppxList != null;
        }
        catch (Exception)
        {
            AllLoaded = false;
        }
    }

    public static bool AllLoaded { get; private set; } = false;

    public static RegistryKey? WinNTCurrent {  get; private set; }

    public static RegistryKey? SystemAppxList { get; private set; }
}
