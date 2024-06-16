using System;
using Microsoft.Win32;

namespace RTWinver.Helpers;

internal class RegistryPath
{
    public RegistryHive RootHive { get; set; }
    public string SubPath { get; set; } = string.Empty;
}

internal static class RegistryHelper
{
    public static bool TryGetRegString(RegistryPath registryPath, string? valueName, out string infoString)
    {
        infoString = string.Empty;
        bool tryGet = false;
        try
        {
            RegistryKey rootKey = RegistryKey.OpenBaseKey(registryPath.RootHive, RegistryView.Default);
            RegistryKey? mainKey = rootKey.OpenSubKey(registryPath.SubPath, false);
            if (mainKey != null)
            {
                object? getValue = mainKey.GetValue(valueName, null);
                if (getValue != null)
                {
                    infoString = (string)getValue;
                    tryGet = true;
                }
            }
        }
        catch (Exception) { }
        return tryGet;
    }

    public static bool TryGetRegDword(RegistryPath registryPath, string? valueName, out uint infoUInt)
    {
        infoUInt = 0;
        bool tryGet = false;
        try
        {
            RegistryKey rootKey = RegistryKey.OpenBaseKey(registryPath.RootHive, RegistryView.Default);
            RegistryKey? mainKey = rootKey.OpenSubKey(registryPath.SubPath, false);
            if (mainKey != null)
            {
                object? getValue = mainKey.GetValue(valueName, null);
                if (getValue != null)
                {
                    infoUInt = Convert.ToUInt32(getValue);
                    tryGet = true;
                }
            }
        }
        catch (Exception) { }
        return tryGet;
    }

    public static bool TryGetSubKeys(RegistryPath registryPath, out string[] subKeyNames)
    {
        subKeyNames = new string[1];
        bool tryGet = false;
        try
        {
            RegistryKey rootKey = RegistryKey.OpenBaseKey(registryPath.RootHive, RegistryView.Default);
            RegistryKey? mainKey = rootKey.OpenSubKey(registryPath.SubPath, false);
            if (mainKey != null)
            {
                subKeyNames = mainKey.GetSubKeyNames();
                tryGet = true;
            }
        }
        catch (Exception) { }
        return tryGet;
    }
}
