using FluentWinver;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.Windows.ApplicationModel.DynamicDependency;
using Microsoft.WindowsAppSDK;
using Microsoft.WindowsAppSDK.Runtime;
using NeonWindows.ApplicationModel;
using NeonWindows.UI.Scaling;
using System.Threading;
using WinRT;

if (!Win32AppModel.IsRunningAsAppX)
{
    PackageVersion minVer = new(Version.Major, Version.Minor, Version.Build, Version.Revision);
    if (!Bootstrap.TryInitialize(Release.MajorMinor, string.Empty, minVer, Bootstrap.InitializeOptions.OnNoMatch_ShowUI, out int hr)) return;
}

Thread appThread = new(AppMain);
appThread.SetApartmentState(ApartmentState.STA);
appThread.Start();
appThread.Join();

static void AppMain()
{
    ComWrappersSupport.InitializeComWrappers();
    AppDpiAwareness2.SetCurrentProcessDpiAwarenessModeEx(DpiAwarenessMode.PerMonitorV2, true);
    Application.Start((p) =>
    {
        var context = new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread());
        SynchronizationContext.SetSynchronizationContext(context);
        AppDpiAwareness2.SetCurrentThreadDpiAwarenessMode(DpiAwarenessMode.PerMonitorV2);
        new App();
    });
}
