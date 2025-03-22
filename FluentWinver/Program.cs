using System;
using System.Threading;
using WinAppModelHelpers;
using WinRT;
using Microsoft.UI.Xaml;
using Microsoft.UI.Dispatching;
using FluentWinver.Helpers;
using System.Text.Json;

namespace FluentWinver;

public static class Program
{
    [STAThread]
    internal static void Main(string[] args)
    {
        if (!AppxEnvironment.IsAppx)
        {
            int hresult;
            bool tryInitWindowsAppRuntime;
            try
            {
                JsonElement waruntimeInfo = MsixRTListHelperUnsafe.GetMsixRuntimeInfoItem("MSIXRuntimeInfo.json", "Microsoft.WindowsAppRuntime");
                uint warMajorMinor = Convert.ToUInt32(MsixRTListHelperUnsafe.MajorMinorVersion(waruntimeInfo).GetString(), 16);
                ulong warMinVersionUl = Convert.ToUInt64(MsixRTListHelperUnsafe.MinVersion(waruntimeInfo).GetString(), 16);
                string warVersionTag = MsixRTListHelperUnsafe.VersionTag(waruntimeInfo).GetString();
                PackageVersion warMinVersion = new(warMinVersionUl);
                tryInitWindowsAppRuntime = WindowsAppRuntimeBootstrap.TryInitialize(warMajorMinor, warVersionTag, warMinVersion,
                    WindowsAppRuntimeBootstrap.InitializeOptions.OnNoMatch_ShowUI, out hresult);
            }
            catch (Exception ex)
            {
                hresult = ex.HResult;
                tryInitWindowsAppRuntime = false;
            }
            if (!tryInitWindowsAppRuntime) Environment.Exit(hresult);
        }
        ComWrappersSupport.InitializeComWrappers();
        Application.Start((p) =>
        {
            var context = new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread());
            SynchronizationContext.SetSynchronizationContext(context);
            new App();
        });
        return;
    }
}
