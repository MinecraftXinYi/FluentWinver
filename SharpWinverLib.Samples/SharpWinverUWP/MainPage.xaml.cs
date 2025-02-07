using SharpWinver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace SharpWinverUWP
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            if (ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.AcrylicBrush"))
                Background = new AcrylicBrush() { BackgroundSource = AcrylicBackgroundSource.HostBackdrop };
            this.InitializeComponent();
            WinEditionContent.Text = Winver.WindowsEdition.WindowsEditionName;
            ReleaseVersionContent.Text = Winver.WindowsVersion.VersionTag;
            OSVersionContent.Text = string.Join(".", Winver.WindowsVersion.OSVersion);
            OSArchContent.Text = Winver.OSArchitecture.ToString();
            Debug.WriteLine(Winver.IsWindowsNT);
            Debug.WriteLine(Winver.WindowsEdition.WindowsSKUName);
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }
    }
}
