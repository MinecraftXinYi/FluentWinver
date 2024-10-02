using SharpWinver.Helpers;
using System;
using System.Runtime.InteropServices;

namespace SharpWinver.Core;

public static class WinNTEdition
{
    static WinNTEdition()
    {
        if (!UsingRegistryKeys.AllLoaded) UsingRegistryKeys.Load();
    }

    public static string ProductName
    {
        get
        {
            RegistryHelper.TryGetRegString(UsingRegistryKeys.WinNTCurrent, "ProductName", out string product);
            return product;
        }
    }

    public static string EditionID
    {
        get
        {
            RegistryHelper.TryGetRegString(UsingRegistryKeys.WinNTCurrent, "EditionID", out string edition);
            return edition;
        }
    }

    public static bool IsWin32DesktopAPISupported
    {
        get
        {
            [DllImport("user32.dll")]
            [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
            static extern IntPtr GetDesktopWindow();

            try
            {
                return GetDesktopWindow() != IntPtr.Zero;
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
