using System;
using System.Threading;
using Windows;
using WinRT;
using Microsoft.Windows.ApplicationModel.DynamicDependency;
using Microsoft.UI.Xaml;
using FluentWinver;
using Microsoft.UI.Dispatching;

namespace FluentWinverProg
{
    public static class Program
    {
        [STAThread]
        internal static void Main(string[] args)
        {
            if (!AppxRuntimeHelper.IsMSIX)
            {
                if (!WARInitializerCs.InitializeWAR(out int hresult))
                {
                    Environment.Exit(hresult);
                }
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
}
