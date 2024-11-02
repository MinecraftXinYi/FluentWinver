using System;
using Microsoft.Win32;

namespace SharpWinver.Helpers;

internal static class RegistryHelper
{
    public static bool TryGetRegString(string keyPath, string? valueName, out string infoString)
    {
        infoString = string.Empty;
        try
        {
            object? getValue = Registry.GetValue(keyPath, valueName, null);
            if (getValue != null)
            {
                infoString = (string)getValue;
                return true;
            }
            else return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool TryGetRegDword(string keyPath, string? valueName, out uint infoUInt)
    {
        infoUInt = 0;
        try
        {
            object? getValue = Registry.GetValue(keyPath, valueName, null);
            if (getValue != null)
            {
                infoUInt = Convert.ToUInt32(getValue);
                return true;
            }
            else return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
