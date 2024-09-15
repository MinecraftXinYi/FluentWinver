﻿using SharpWinver.Constants;
using SharpWinver.Core;
using SharpWinver.Helpers;
using System;
using System.Linq;

namespace SharpWinver.CoreExternal;

internal static class WinClientCBS
{
    public static bool IsClientCBSPackageNeeded
    {
        get => RtlNtVersion.WinNTVersion.Build >= 19041 && WinNTEdition.IsWin32DesktopAPISupported;
    }

    public static string ClientCBSPackageVersion
    {
        get
        {
            try
            {
                RegistryHelper.TryGetSubKeyList(NativeRegPaths.SystemAppxList, out string[] subKeys);
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
