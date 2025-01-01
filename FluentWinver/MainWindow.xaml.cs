// <summary>
// Fluent Winver
// A WinUI3 version of winver.
// Version: 1.0.0
// Made with Visual Studio 2022
// Used Projects: WinverUWP.Interop
//
// Current: MainWindow.Code
// </summary>

using System;
using SharpWinver;
using SharpWinverEx;
using WinverUWP.Helpers;
using Win32;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.ApplicationModel.DataTransfer;
using Microsoft.Windows.ApplicationModel.Resources;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.UI.Xaml.Markup;
using Windows.UI.ViewManagement;
using Microsoft.UI.Composition;
using System.Globalization;
using System.Windows;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentWinver
{
    public sealed partial class MainWindow : Window
    {
        //Private Classes
        private readonly ResourceLoader m_resourceLoader;
        private readonly CultureInfo userCulture;

        //Main Codes
        public MainWindow()
        {
            //Resources API Preloading
            m_resourceLoader = new ResourceLoader();

            //Get current user culture info
            userCulture = CultureInfoHelper.GetCurrentCulture();

            //���ؽ������
            this.InitializeComponent();

            //��ȡ���ھ��(HWND)
            IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            //��ʼ������
            Title = m_resourceLoader.GetString("MainWindowTitle");
            User32Packaged.SetWindowSizeByScalingFactor(hwnd, 550, 850);
            AppWindow.TitleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;

            //���ز���ʾϵͳ��Ϣ
            this.LoadMain();
        }

        //��Ҫ�ļ��ش���
        void LoadMain()
        {
            //����ϵͳ����
            string osEdition = Winver.WindowsEdition;
            OSEditionBlock.Text = osEdition;

            //����ϵͳ�汾��
            OSVersionBlock.Text = Winver.WindowsVersion.ReleaseVersionTag;

            //Get the build number of current OS
            uint buildNum = Winver.WindowsVersion.Build;

            //����ϵͳ�ڲ��汾��
            OSBuildVersionBlock.Text = Winver.WindowsVersion.FullVersionTag;

            //����ϵͳ������汾
            if (WindowsUXInfo.UsingClientCBSExperience)
                OSExperienceBlock.Text = $"{WindowsUXInfo.ClientCBSPackName} {WindowsUXInfo.ClientCBSVersion}";
            else
            {
                OSExperienceHeader.Visibility = Visibility.Collapsed;
                OSExperienceBlock.Visibility = Visibility.Collapsed;
            }

            //����ϵͳƽ̨�ܹ�
            OSArchBlock.Text = Winver.OSArchitecture;

            //����ϵͳ��װʱ��
            OSInstalledDateBlock.Text = Winver.WinInstallationInfo.OSInstallationDateTime.ToString("d", userCulture);

            //����ϵͳ�����̣���Ȩ��������
            OSCopyRightBlock.Text = Winver.WinOSLegalInfo.OSCopyrightString;

            //����ϵͳ���Ը�������ʱ�������
            if (Winver.WinOSTestBuildInfo.HasExpirationTime)
                OSExpirationTimeBlock.Text = Winver.WinOSTestBuildInfo.OSExpirationTime!.Value.ToString("g", userCulture);
            else
            {
                OSExpirationTimeHeader.Visibility = Visibility.Collapsed;
                OSExpirationTimeBlock.Visibility = Visibility.Collapsed;
            }

            //���� Windows Legal ��Ϣ
            LicensingTextBlock.Text = m_resourceLoader.GetString("LicensingText").Replace("[Microsoft Windows]", osEdition);

            //����ϵͳע���û���
            RegisterBlock.Text = Winver.WinInstallationInfo.OSRegisteredOwner;

            //LicenseLinkButton ���ӳ�ʼ��
            LicenseLinkButton.NavigateUri = new Uri(m_resourceLoader.GetString("LicenseLink"));

            //���� Windows Brand
            string osShortName = buildNum >= 21996 ? "Windows11" : "Windows10";
            UpdateWindowsBrand(osShortName, new UISettings());

            //����ͼ��
            this.LoadWindowIcon(buildNum);
        }

        //����ͼ��ļ��ش���
        void LoadWindowIcon(uint buildNum)
        {
            if (buildNum >= 21996)
            {
                AppWindow.SetIcon("Assets/@WLOGO_11.ico");
            }
            else
            {
                AppWindow.SetIcon("Assets/@WLOGO_10.ico");
            }
        }

        //Windows Brand �ļ��ش���
        private void UpdateWindowsBrand(string osShortName, UISettings uiSettings)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.Composition.CompositionShape"))
            {
                using FirstDisposableTuple<CanvasPathBuilder, float, float> path = osShortName == "Windows11" ? OSPathsHelper.GetWindows11Path() : OSPathsHelper.GetWindows10Path();
                using CanvasGeometry canvasGeo = CanvasGeometry.CreatePath(path.Item1);
                CompositionPath compPath = new(canvasGeo);
                var compositor = Compositor;
                var compGeo = compositor.CreatePathGeometry(compPath);
                var shape = compositor.CreateSpriteShape(compGeo);
                shape.FillBrush = compositor.CreateColorBrush(uiSettings.GetColorValue(UIColorType.Foreground));
                var shapeVisual = compositor.CreateShapeVisual();
                shapeVisual.Shapes.Add(shape);
                shapeVisual.Size = new(path.Item2, path.Item3);
                Microsoft.UI.Xaml.Hosting.ElementCompositionPreview.SetElementChildVisual(CompatibleCanvas, shapeVisual);
                CompatibleCanvas.Width = path.Item2;
                CompatibleCanvas.Height = path.Item3;
            }
            else
            {
                string path = (string)Application.Current.Resources[$"{osShortName}Path"];
                var geo = (Geometry)XamlBindingHelper.ConvertValue(typeof(Geometry), path);
                NonCompatiblePath.Data = geo;
            }
        }

        //���� Windows �����Ϣ��������
        private void Copy_WS(object sender, RoutedEventArgs e)
        {
            string[] strings;
            if(Winver.WinOSTestBuildInfo.HasExpirationTime)
            {
                strings =
                [
                    $"{OSEditionHeader.Text} {OSEditionBlock.Text}",
                    $"{OSVersionHeader.Text} {OSVersionBlock.Text}",
                    $"{OSBuildVersionHeader.Text} {OSBuildVersionBlock.Text}",
                    $"{OSArchHeader.Text} {OSArchBlock.Text}",
                    $"{OSInstalledDateHeader.Text} {OSInstalledDateBlock.Text}",
                    $"{OSExperienceHeader.Text} {OSExperienceBlock.Text}",
                    $"{OSExpirationTimeHeader.Text} {OSExpirationTimeBlock.Text}"
                ];
            }
            else
            {
                strings =
                [
                    $"{OSEditionHeader.Text} {OSEditionBlock.Text}",
                    $"{OSVersionHeader.Text} {OSVersionBlock.Text}",
                    $"{OSBuildVersionHeader.Text} {OSBuildVersionBlock.Text}",
                    $"{OSArchHeader.Text} {OSArchBlock.Text}",
                    $"{OSInstalledDateHeader.Text} {OSInstalledDateBlock.Text}",
                    $"{OSExperienceHeader.Text} {OSExperienceBlock.Text}"
                ];
            }
            var dataPackage = new DataPackage();
            dataPackage.SetText(string.Join("\r\n", strings));
            Clipboard.SetContentWithOptions(dataPackage, new ClipboardContentOptions() { IsRoamable = true });
            Clipboard.Flush();
        }

        //��ȷ������ť�¼�
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
