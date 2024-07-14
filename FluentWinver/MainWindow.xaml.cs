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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentWinver
{
    public sealed partial class MainWindow : Window
    {
        //Private Classes
        private ResourceLoader m_resourceLoader;
        private CultureInfo userCulture;

        //Main Codes
        public MainWindow()
        {
            //Resources API Preloading
            m_resourceLoader = new ResourceLoader();

            //Get current user culture info
            userCulture = CultureInfoHelper.GetCurrentCulture();

            //加载界面组件
            this.InitializeComponent();

            //获取窗口句柄(HWND)
            IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            //初始化窗口
            Title = m_resourceLoader.GetString("MainWindowTitle");
            User32Windowing.SetWindowSizeByScalingFactor(hwnd, 550, 850);

            //加载并显示系统信息
            this.LoadMain();
        }

        //主要的加载代码
        void LoadMain()
        {
            //加载系统名称
            string osEdition = OSEdition.OSEditionName;
            OSEditionBlock.Text = osEdition;

            //加载系统版本号
            OSVersionBlock.Text = OSVersion.ReleaseVersion;

            //Get the build number of current OS
            int buildNum = int.Parse(OSVersion.Revision);

            //加载系统内部版本号
            OSBuildVersionBlock.Text = OSVersion.FullVersion;

            //OS Experience
            OSExperienceBlock.Text = $"{OSExperienceInfo.CBSPackName} {OSExperienceInfo.CBSVersion}";

            //加载系统架构
            OSArchBlock.Text = OSEdition.OSArchitecture;

            //加载系统安装时间
            OSInstalledDateBlock.Text = OSInstallationInfo.OSInstallationDateTime.ToString("d", userCulture);

            //加载系统开发商（版权方）名称
            OSCopyRightBlock.Text = OSLegalInfo.OSCopyRightString;

            //加载系统测试副本过期时间或隐藏
            if (OSTestBuildCheck.HasExpirationTime)
                OSExpirationTimeBlock.Text = OSTestBuildCheck.OSExpirationTime.ToString("g", userCulture);
            else
            {
                OSExpirationTimeHeader.Visibility = Visibility.Collapsed;
                OSExpirationTimeBlock.Visibility = Visibility.Collapsed;
            }

            //加载 Windows Legal 信息
            LicensingTextBlock.Text = m_resourceLoader.GetString("LicensingText").Replace("[Microsoft Windows]", osEdition);

            //加载系统注册用户名
            RegisterBlock.Text = OSLegalInfo.OSRegisteredUser;

            //LicenseLinkButton 链接初始化
            LicenseLinkButton.NavigateUri = new Uri(m_resourceLoader.GetString("LicenseLink"));

            //加载 Windows Brand
            string osShortName = buildNum >= 21996 ? "Windows11" : "Windows10";
            UpdateWindowsBrand(osShortName, new UISettings());

            //窗口图标
            this.LoadWindowIcon(buildNum);
        }

        //窗口图标加载
        void LoadWindowIcon(int buildNum)
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

        //Windows Brand 加载
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

        //复制 Windows 规格信息到剪贴板
        private void Copy_WS(object sender, RoutedEventArgs e)
        {
            string[] strings;
            if(OSTestBuildCheck.HasExpirationTime)
            {
                strings = new string[7]
                {
                    OSEditionHeader.Text + "  " + OSEditionBlock.Text,
                    OSVersionHeader.Text + "  " + OSVersionBlock.Text,
                    OSBuildVersionHeader.Text + "  " + OSBuildVersionBlock.Text,
                    OSArchHeader.Text + "  " + OSArchBlock.Text,
                    OSInstalledDateHeader.Text + "  " + OSInstalledDateBlock.Text,
                    OSExperienceHeader.Text + "  " +OSExperienceBlock.Text,
                    OSExpirationTimeHeader.Text + "  " + OSExpirationTimeBlock.Text
                };
            }
            else
            {
                strings = new string[6]
                {
                    OSEditionHeader.Text + "  " + OSEditionBlock.Text,
                    OSVersionHeader.Text + "  " + OSVersionBlock.Text,
                    OSBuildVersionHeader.Text + "  " + OSBuildVersionBlock.Text,
                    OSArchHeader.Text + "  " + OSArchBlock.Text,
                    OSInstalledDateHeader.Text + "  " + OSInstalledDateBlock.Text,
                    OSExperienceHeader.Text + "  " +OSExperienceBlock.Text
                };
            }
            var dataPackage = new DataPackage();
            dataPackage.SetText(string.Join("\r\n", strings));
            Clipboard.SetContent(dataPackage);
        }

        //“确定”按钮事件
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
