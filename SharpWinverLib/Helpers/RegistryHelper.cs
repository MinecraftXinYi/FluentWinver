using System;
using Microsoft.Win32;

namespace SharpWinver.Helpers;

internal static class RegistryHelper
{
    public static bool TryGetRegString(RegistryKey? registryKey, string? valueName, out string infoString)
    {
        infoString = string.Empty;
        if (registryKey != null)
        {
            try
            {
                object getValue = registryKey.GetValue(valueName, null);
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
        else return false;
    }

    public static bool TryGetRegDword(RegistryKey? registryKey, string? valueName, out uint infoUInt)
    {
        infoUInt = 0;
        if (registryKey != null)
        {
            try
            {
                object getValue = registryKey.GetValue(valueName, null);
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
        else return false;
    }

    public static bool TryGetSubKeyList(RegistryKey? registryKey, out string[] subKeyNames)
    {
        subKeyNames = new string[1];
        if (registryKey != null)
        {
            try
            {
                subKeyNames = registryKey.GetSubKeyNames();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        else return false;
    }
}
