using Microsoft.Win32;
using SharpWinver;
using System;
using System.Linq;
using Windows.System.Profile;

namespace FluentWinver.Helpers;

internal static class WinCBSPackInfoHelper
{
    public static bool IsCBSPackSupported
    {
        get => Winver.WindowsVersion.OSVersion.Build >= 19041;
    }

    public static bool IsClientCBSPackageSupported
    {
        get => IsCBSPackSupported && AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Desktop";
    }

    public const string ClientCBSPackName = "Windows Feature Experience Pack";

    public static string ClientCBSPackageVersionString
    {
        get
        {
            try
            {
                RegistryKey systemAppxList = Registry.LocalMachine.OpenSubKey(
                    "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\InboxApplications",
                    false);
                string[] subkeyNames = systemAppxList.GetSubKeyNames();
                string cbs = subkeyNames.First(f => f.StartsWith("MicrosoftWindows.Client.CBS_",
                    StringComparison.CurrentCultureIgnoreCase));
                return cbs.Split('_')[1];
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
