using SharpWinver.Helpers;
using SharpWinver.Models.Constants;
using System;
using System.Runtime.InteropServices;

namespace SharpWinver.Core;

public static class WinNTEdition
{
    public static string ProductName
    {
        get
        {
            RegistryHelper.TryGetRegString(RegistryPaths.WinNTCurrent, "ProductName", out string product);
            return product;
        }
    }

    public static string EditionID
    {
        get
        {
            RegistryHelper.TryGetRegString(RegistryPaths.WinNTCurrent, "EditionID", out string edition);
            return edition;
        }
    }

    public static bool IsDesktopPlatform
    {
        get
        {
            [DllImport("user32.dll")]
            [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
            static extern IntPtr GetDesktopWindow();

            try
            {
                if (GetDesktopWindow() != IntPtr.Zero) return true;
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public static string PlatformArchitecture
    {
        get => RuntimeInformation.OSArchitecture.ToString();
    }
}
