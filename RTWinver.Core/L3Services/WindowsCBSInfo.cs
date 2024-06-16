using RTWinver.Helpers;
using RTWinver.L2Services;
using RTWinver.Services;
using System;
using System.Linq;

namespace RTWinver.L3Services;

internal static class WindowsCBSInfo
{
    public static bool UsesCBSPackage
    {
        get => OSVersionRaw.OSVersionRevision >= 19041 && OSTypeInfo.IsDesktopPlatform;
    }

    public static string CBSPackageVersion
    {
        get
        {
            try
            {
                RegistryHelper.TryGetSubKeyList(RegistryPaths.SystemAppxList, out string[] subKeys);
                string cbs = subKeys.First(f => f.StartsWith("MicrosoftWindows.Client.CBS_", StringComparison.CurrentCultureIgnoreCase));
                string cbsVersion = cbs.Split('_')[1];
                return cbsVersion;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
