using System;
using Microsoft.Win32;

namespace RTWinver.Services;

public static class RegistryHelper
{
    public static string NTInfoKeyPath
    {
        get
        {
            return @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion";
        }
    }

    public static bool TryGetInfoString(string keyName, string? valueName, out string infoString)
    {
        infoString = string.Empty;
        bool tryGet = false;
        try
        {
            object? getValue = Registry.GetValue(keyName, valueName, null);
            if (getValue != null)
            {
                infoString = (string)getValue;
                tryGet = true;
            }
        }
        catch (Exception) { }
        return tryGet;
    }

    public static bool TryGetInfoDword(string keyName, string? valueName, out uint infoUInt)
    {
        infoUInt = 0;
        bool tryGet = false;
        try
        {
            object? getValue = Registry.GetValue(keyName, valueName, null);
            if (getValue != null)
            {
                infoUInt = Convert.ToUInt32(getValue);
                tryGet = true;
            }
        }
        catch (Exception) { }
        return tryGet;
    }
}
