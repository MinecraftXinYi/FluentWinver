using System;
using SharpWinver;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel.DataTransfer;
using Microsoft.Windows.ApplicationModel.Resources;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.UI.Xaml.Markup;
using Windows.UI.ViewManagement;
using Microsoft.UI.Composition;
using System.Globalization;
using FluentWinver.Helpers;
using System.Collections.Generic;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentWinver;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        ResourceLoader resourceLoader = new();
        InitWindow(resourceLoader);
        this.InitializeComponent();
        LoadMain(resourceLoader, CultureInfoHelper.GetCurrentCulture(), out Version osVersion);
        uint buildNum = (uint)osVersion.Build;
        LoadWindowIcon(buildNum);
        LoadWindowsBrand(buildNum);
        LoadBackdrop(buildNum);
        LoadLast(resourceLoader);
    }

    private void Copy_WS(object sender, RoutedEventArgs e)
    {
        this.CopyInfoToClipboard();
    }

    private void OK_Click(object sender, RoutedEventArgs e)
    {
        this.CloseView();
    }

    void InitWindow(ResourceLoader resourceLoader)
    {
        //��ȡ���ھ��(HWND)
        IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

        //��ʼ������
        Title = resourceLoader.GetString("MainWindowTitle");
        User32Packaged.SetWindowSizeByScalingFactor(hwnd, 550, 850);
        AppWindow.TitleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;
    }

    void LoadMain(ResourceLoader resourceLoader, CultureInfo userCulture, out Version osVersion)
    {
        //����ϵͳ����
        string osEdition = Winver.WindowsEdition.OSEdition;
        OSEditionBlock.Text = osEdition;

        //����ϵͳ�汾��
        OSVersionTagBlock.Text = Winver.WindowsVersion.VersionTag;

        osVersion = Winver.WindowsVersion.OSVersion;


        //����ϵͳ�ڲ��汾��
        OSVersionBlock.Text = osVersion.ToString();

        //����ϵͳ������汾
        if (WinCBSPackInfoHelper.IsClientCBSPackageSupported)
            OSExperienceBlock.Text = $"{WinCBSPackInfoHelper.ClientCBSPackName} {WinCBSPackInfoHelper.ClientCBSPackageVersionString}";
        else
        {
            OSExperienceBlock.Text = string.Empty;
            OSExperienceHeader.Visibility = Visibility.Collapsed;
            OSExperienceBlock.Visibility = Visibility.Collapsed;
        }

        //����ϵͳƽ̨�ܹ�
        OSArchBlock.Text = Winver.WindowsEdition.OSArchitecture.ToString();

        //����ϵͳ��װʱ��
        OSInstalledDateBlock.Text = Winver.WinInstallationInfo.OSInstallationDateTime.ToLocalTime().ToString("d", userCulture);

        //����ϵͳ�����̣���Ȩ��������
        OSCopyRightBlock.Text = Winver.WinOSLegalInfo.OSCopyrightString;

        //����ϵͳ���Ը�������ʱ�������
        if (Winver.WinOSTestBuildInfo.HasExpirationTime)
            OSExpirationTimeBlock.Text = Winver.WinOSTestBuildInfo.OSExpirationTime!.Value.ToLocalTime().ToString("g", userCulture);
        else
        {
            OSExpirationTimeHeader.Visibility = Visibility.Collapsed;
            OSExpirationTimeBlock.Visibility = Visibility.Collapsed;
        }

        //���� Windows Legal ��Ϣ
        LicensingTextBlock.Text = resourceLoader.GetString("LicensingText").Replace("[Microsoft Windows]", osEdition);

        //����ϵͳע���û���
        RegisterBlock.Text = Winver.WinInstallationInfo.OSRegisteredOwner;
    }

    void LoadWindowIcon(uint buildNum)
    {
        //���ش���ͼ��
        string icoPath = buildNum >= 21996 ? "Assets/@WLOGO_11.ico" : "Assets/@WLOGO_10.ico";
        AppWindow.SetIcon(icoPath);
    }

    void LoadWindowsBrand(uint buildNum)
    {
        //���� Windows Brand
        string osShortName = buildNum >= 21996 ? "Windows11" : "Windows10";
        UpdateWindowsBrand(osShortName, new UISettings());
    }

    void LoadBackdrop(uint buildNum)
    {
        //��� Mica �� Acrylic ����
        SystemBackdrop backdrop = buildNum >= 21996 ? new MicaBackdrop() : new DesktopAcrylicBackdrop();
        SystemBackdrop = backdrop;
    }

    void LoadLast(ResourceLoader resourceLoader)
    {
        //LicenseLinkButton ���ӳ�ʼ��
        LicenseLinkButton.NavigateUri = new Uri(resourceLoader.GetString("LicenseLink"));
    }

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

    void CopyInfoToClipboard()
    {
        List<string> strings =
        [
                $"{OSEditionHeader.Text} {OSEditionBlock.Text}",
                $"{OSVersionTagHeader.Text} {OSVersionTagBlock.Text}",
                $"{OSVersionHeader.Text} {OSVersionBlock.Text}",
                $"{OSArchHeader.Text} {OSArchBlock.Text}",
                $"{OSInstalledDateHeader.Text} {OSInstalledDateBlock.Text}",
                $"{OSExperienceHeader.Text} {OSExperienceBlock.Text}",
        ];
        if (!string.IsNullOrEmpty(OSExperienceBlock.Text))
            strings.Add($"{OSExpirationTimeHeader.Text} {OSExpirationTimeBlock.Text}");
        DataPackage dataPackage = new();
        dataPackage.SetText(string.Join("\r\n", strings));
        Clipboard.SetContentWithOptions(dataPackage, new ClipboardContentOptions() { IsRoamable = true });
        Clipboard.Flush();
    }

    void CloseView() => this.Close();
}
