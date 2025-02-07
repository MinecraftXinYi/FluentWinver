using Microsoft.Win32;
using SharpWinver;
using System;
using System.Linq;
using Windows.System.Profile;

namespace SharpWinverEx.CoreExternal;

internal static class WinClientCBSInf
{
    public static bool IsClientCBSPackageNeeded
    {
        get => Winver.WindowsVersion.OSVersion.Build >= 19041 && AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Desktop";
    }

    public static string ClientCBSPackageVersion
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
