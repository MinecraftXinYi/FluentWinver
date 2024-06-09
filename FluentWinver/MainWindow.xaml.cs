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

            //���ؽ������
            this.InitializeComponent();

            //��ʼ������
            _uiSettings = new UISettings();

            Title = m_resourceLoader.GetString("AppTitle");
            this.ShapeWindow();
            this.LoadWindowIcon();

            //���� Windows Brand
            this.LoadWindowsBrand();

            //���ز���ʾϵͳ��Ϣ
            this.LoadMainInfo();
            this.LoadMoreInfo();

            //Load License
            this.LoadLicense();
        }

        //[Windows���]���ش���ʵ��
        void LoadMainInfo()
        {
            //����ϵͳ����
            OSEditionBlock.Text = OSEdition.OSEditionName;

            //����ϵͳ�汾��
            OSVersionBlock.Text = OSVersion.DisplayVersion;

            //����ϵͳ�ڲ��汾��
            OSBuildVersionBlock.Text = OSVersion.FullVersion;

            //����ϵͳ�ܹ�
            OSArchBlock.Text = OSEdition.OSArchitecture;

            //����ϵͳ�����̣���Ȩ��������
            OSCopyRightBlock.Text = OSLegalInfo.OSCopyRightString;

            //����ϵͳ���Ը�������ʱ�������
            if (TestBuildCheck.HasExpirationTime)
                OSExpirationTimeBlock.Text = TestBuildCheck.OSExpirationTime.ToString("g", userCulture);
            else
            {
                ExpirationHeader.Visibility = Visibility.Collapsed;
                OSExpirationTimeBlock.Visibility = Visibility.Collapsed;
            }
        }

        //[������Ϣ]���ش���ʵ��
        void LoadMoreInfo()
        {
            //����ϵͳ��װʱ��
            InstalledDateBlock.Text = OSEnvironmentInfo.OSInstallationDateTime.ToString("d", userCulture);

            //����ϵͳע���û���
            RegisterBlock.Text = OSLegalInfo.OSRegisteredUser;

            //����ϵͳĿ¼
            SysdirecBlock.Text = OSEnvironmentInfo.OSDirectory;
        }

        //Windows Brand ����
        void LoadWindowsBrand()
        {
            //int BuildNum
            int BuildNum = int.Parse(OSVersion.Revision);

            //���� Windows Brand
            OSName = BuildNum >= 21996 ? "Windows11" : "Windows10";
            UpdateWindowsBrand();
        }

        //Shape this window
        void ShapeWindow()
        {
            //��ȡ���ھ��(HWND)
            IntPtr HWND = WinRT.Interop.WindowNative.GetWindowHandle(this);

            [DllImport("user32.dll", SetLastError = true)]
            static extern uint GetDpiForWindow(IntPtr hWnd);

            //����DPI���������ڴ�С
            var dpi = (int)GetDpiForWindow(HWND);
            float scalingFactor = (float)dpi / 96;

            var size = new Windows.Graphics.SizeInt32();
            size.Width = (int)(550 * scalingFactor);
            size.Height = (int)(850 * scalingFactor);

            AppWindow.Resize(size);
        }

        //����ͼ�����
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

        //License һ������
        void LoadLicense()
        {
            //LicenseLinkButton ���ӳ�ʼ��
            LicenseLinkButton.NavigateUri = new Uri(m_resourceLoader.GetString("LicenseLink"));
        }

        //Windows Brand ֧��
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

        //���� Windows �����Ϣ��������
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

        //��ȷ������ť�¼�
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
