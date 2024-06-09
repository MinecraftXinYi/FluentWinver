// <summary>
// Fluent Winver
// A WinUI3 version of winver.
// Version: 2.5.0
// Made with Visual Studio 2022
// Used Projects: WinverUWP.Interop
//
// Current: MainWindow.Code
// </summary>

using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using WinverUWP.Helpers;
using Windows.ApplicationModel.DataTransfer;
using Microsoft.Windows.ApplicationModel.Resources;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.UI.Xaml.Markup;
using Windows.UI.ViewManagement;
using Microsoft.UI.Composition;
using System.Runtime.InteropServices;
using RTWinver;
using System.Globalization;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentWinver
{
    
    public sealed partial class MainWindow : Window
    {
        //Private Classes
        private ResourceLoader m_resourceLoader;

        private UISettings _uiSettings;
        private CultureInfo userCulture;
        private string OSName = "";

        //Main Codes
        public MainWindow(ResourceLoader resourceLoader)
        {
            //Resources API Preloading
            m_resourceLoader = resourceLoader;

            //Load current user culture info
            userCulture = CultureInfoHelper.GetCurrentCulture();

            //加载界面组件
            this.InitializeComponent();

            //初始化窗口
            _uiSettings = new UISettings();

            Title = m_resourceLoader.GetString("AppTitle");
            this.ShapeWindow();
            this.LoadWindowIcon();

            //加载 Windows Brand
            this.LoadWindowsBrand();

            //加载并显示系统信息
            this.LoadMainInfo();
            this.LoadMoreInfo();

            //Load License
            this.LoadLicense();
        }

        //[Windows规格]加载代码实现
        void LoadMainInfo()
        {
            //加载系统名称
            OSEditionBlock.Text = OSEdition.OSEditionName;

            //加载系统版本号
            OSVersionBlock.Text = OSVersion.DisplayVersion;

            //加载系统内部版本号
            OSBuildVersionBlock.Text = OSVersion.FullVersion;

            //加载系统架构
            OSArchBlock.Text = OSEdition.OSArchitecture;

            //加载系统开发商（版权方）名称
            OSCopyRightBlock.Text = OSLegalInfo.OSCopyRightString;

            //加载系统测试副本过期时间或隐藏
            if (TestBuildCheck.HasExpirationTime)
                OSExpirationTimeBlock.Text = TestBuildCheck.OSExpirationTime.ToString("g", userCulture);
            else
            {
                ExpirationHeader.Visibility = Visibility.Collapsed;
                OSExpirationTimeBlock.Visibility = Visibility.Collapsed;
            }
        }

        //[更多信息]加载代码实现
        void LoadMoreInfo()
        {
            //加载系统安装时间
            InstalledDateBlock.Text = OSEnvironmentInfo.OSInstallationDateTime.ToString("d", userCulture);

            //加载系统注册用户名
            RegisterBlock.Text = OSLegalInfo.OSRegisteredUser;

            //加载系统目录
            SysdirecBlock.Text = OSEnvironmentInfo.OSDirectory;
        }

        //Windows Brand 加载
        void LoadWindowsBrand()
        {
            //int BuildNum
            int BuildNum = int.Parse(OSVersion.Revision);

            //加载 Windows Brand
            OSName = BuildNum >= 21996 ? "Windows11" : "Windows10";
            UpdateWindowsBrand();
        }

        //Shape this window
        void ShapeWindow()
        {
            //获取窗口句柄(HWND)
            IntPtr HWND = WinRT.Interop.WindowNative.GetWindowHandle(this);

            [DllImport("user32.dll", SetLastError = true)]
            static extern uint GetDpiForWindow(IntPtr hWnd);

            //根据DPI调整主窗口大小
            var dpi = (int)GetDpiForWindow(HWND);
            float scalingFactor = (float)dpi / 96;

            var size = new Windows.Graphics.SizeInt32();
            size.Width = (int)(550 * scalingFactor);
            size.Height = (int)(850 * scalingFactor);

            AppWindow.Resize(size);
        }

        //窗口图标加载
        void LoadWindowIcon()
        {
            //Int BuildNum
            int BuildNum = int.Parse(OSVersion.Revision);

            //Set icon of this window
            if (BuildNum >= 21996)
            {
                AppWindow.SetIcon("Assets/@WLOGO_11.ico");
            }
            else
            {
                AppWindow.SetIcon("Assets/@WLOGO_10.ico");
            }
        }

        //License 一栏加载
        void LoadLicense()
        {
            //LicenseLinkButton 链接初始化
            LicenseLinkButton.NavigateUri = new Uri(m_resourceLoader.GetString("LicenseLink"));
        }

        //Windows Brand 支持
        private void UpdateWindowsBrand()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.Composition.CompositionShape"))
            {
                using FirstDisposableTuple<CanvasPathBuilder, float, float> path = OSName == "Windows11" ? OSPathsHelper.GetWindows11Path() : OSPathsHelper.GetWindows10Path();
                using CanvasGeometry canvasGeo = CanvasGeometry.CreatePath(path.Item1);
                CompositionPath compPath = new(canvasGeo);
                var compositor = Compositor;
                var compGeo = compositor.CreatePathGeometry(compPath);
                var shape = compositor.CreateSpriteShape(compGeo);
                shape.FillBrush = compositor.CreateColorBrush(_uiSettings.GetColorValue(UIColorType.Foreground));
                var shapeVisual = compositor.CreateShapeVisual();
                shapeVisual.Shapes.Add(shape);
                shapeVisual.Size = new(path.Item2, path.Item3);
                Microsoft.UI.Xaml.Hosting.ElementCompositionPreview.SetElementChildVisual(CompatibleCanvas, shapeVisual);
                CompatibleCanvas.Width = path.Item2;
                CompatibleCanvas.Height = path.Item3;
            }
            else
            {
                string path = (string)Application.Current.Resources[$"{OSName}Path"];
                var geo = (Geometry)XamlBindingHelper.ConvertValue(typeof(Geometry), path);
                NonCompatiblePath.Data = geo;
            }
        }

        //复制 Windows 规格信息到剪贴板
        private void Copy_WS(object sender, RoutedEventArgs e)
        {
            var package = new DataPackage();
            if(TestBuildCheck.HasExpirationTime)
            {
                package.SetText(OSEditionBlock.Text + "\r\n" + OSVersionHeader.Text + "  " + OSVersionBlock.Text + "\r\n" + OSBuildVersionHeader.Text + "  " + OSBuildVersionBlock.Text + "\r\n" + OSArchHeader.Text + "  " + OSArchBlock.Text + "\r\n" + ExpirationHeader.Text + "  " + OSExpirationTimeBlock.Text);
            }
            else
            {
                package.SetText(OSEditionBlock.Text + "\r\n" + OSVersionHeader.Text + "  " + OSVersionBlock.Text + "\r\n" + OSBuildVersionHeader.Text + "  " + OSBuildVersionBlock.Text + "\r\n" + OSArchHeader.Text + "  " + OSArchBlock.Text);
            }
            Clipboard.SetContent(package);

        }

        //“确定”按钮事件
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
