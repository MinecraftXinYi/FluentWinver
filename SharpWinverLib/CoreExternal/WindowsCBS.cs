using SharpWinver.Core;
using SharpWinver.Helpers;
using SharpWinver.Models.Constants;
using System;
using System.Linq;

namespace SharpWinver.CoreExternal;

internal static class WindowsCBS
{
    public static bool UsesCBSPackage
    {
        get => RtlNtVersion.WinNTVersion.Build >= 19041 && WinNTEdition.IsDesktopPlatform;
    }

    public static string CBSPackageVersion
    {
        get
        {
            try
            {
                RegistryHelper.TryGetSubKeyList(RegistryPaths.SystemAppxList, out string[] subKeys);
                string cbs = subKeys.First(f => f.StartsWith("MicrosoftWindows.Client.CBS_", StringComparison.CurrentCultureIgnoreCase));
                return cbs.Split('_')[1];
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
