using FluentWinver.Helpers;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.UI.Composition;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Windows.ApplicationModel.Resources;
using SharpWinver;
using SharpWinver.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using Windows.UI.ViewManagement;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FluentWinver;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public IWinVer WinVer { get; set; } = new OnlineWinVer();

    private readonly ResourceLoader resourceLoader = new();

    private readonly UISettings uiSettings = new();

    private static readonly Version win11min = new(10, 0, 21996);

    public MainWindow()
    {
        InitializeComponent();
        LoadWindowStyle();
        LoadMainInfo();
        LoadWindowsBrand();
        LoadWindowStyle2();
    }

    private void Copy_WS(object sender, RoutedEventArgs e)
    {
        CopyInfoToClipboard();
    }

    private void OK_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    public void LoadMainInfo()
    {
        OSEditionBlock.Text = WinVer.Edition;
        OSVersionTagBlock.Text = WinVer.VersionTag;
        OSVersionBlock.Text = WinVer.OSVersion.ToString();
        OSArchBlock.Text = WinVer.OSArchitecture.ToString();

        OSInstalledDateBlock.Text = OnlineWinInstallationInfo.SystemInstallationDateTime?.ToString("D");
        if (OnlineWinTestBuildInfo.SystemExpirationDateTime.HasValue) OSExpirationTimeBlock.Text = OnlineWinTestBuildInfo.SystemExpirationDateTime.Value.ToLocalTime().ToString();
        else
        {
            OSExpirationTimeHeader.Visibility = Visibility.Collapsed;
            OSExpirationTimeBlock.Visibility = Visibility.Collapsed;
        }

        OSCopyRightBlock.Text = OnlineWinProductInfo.WindowsCopyrightString;
        LicensingTextBlock.Text = resourceLoader.GetString("LicensingText").Replace("[Microsoft Windows]", WinVer.Edition);
        RegisterBlock.Text = OnlineWinInstallationInfo.SystemRegisteredOwner;
        LicenseLinkButton.NavigateUri = new(resourceLoader.GetString("LicenseLink"));
    }

    public void LoadWindowStyle()
    {
        Title = resourceLoader.GetString("MainWindowTitle");
        AppWindow.TitleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;
        double scale = Content.RasterizationScale;
        int widthRaw = int.Parse(resourceLoader.GetString("MainWindowWidth")), heightRaw = int.Parse(resourceLoader.GetString("MainWindowHeight"));
        SizeInt32 size = new(widthRaw * (int)scale, heightRaw * (int)scale);
        AppWindow.Resize(size);
    }

    public void LoadWindowStyle2()
    {
        AppWindow.SetIcon(WinVer.OSVersion >= win11min ? "Assets/@WLOGO_11.ico" : "Assets/@WLOGO_10.ico");
        if (WinVer.OSVersion >= win11min) SystemBackdrop = new MicaBackdrop() { Kind = MicaKind.BaseAlt };
        else SystemBackdrop = new DesktopAcrylicBackdrop();
    }

    public void LoadWindowsBrand()
    {
        using FirstDisposableTuple<CanvasPathBuilder, float, float> path = WinVer.OSVersion >= win11min ? OSPathsHelper.GetWindows11Path() : OSPathsHelper.GetWindows10Path();
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

    public void CopyInfoToClipboard()
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
        Clipboard.SetContentWithOptions(dataPackage, new() { IsRoamable = true });
        Clipboard.Flush();
    }
}
