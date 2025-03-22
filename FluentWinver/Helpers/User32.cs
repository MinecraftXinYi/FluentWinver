using System.Runtime.InteropServices;

namespace FluentWinver.Helpers;

//Native Win32 Methods
public static partial class User32
{
#pragma warning disable CA1401
    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint GetDpiForWindow(nint hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SetWindowPos(nint hWnd, nint hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);
}

//Packaged Methods
public static partial class User32Packaged
{
    public static void SetWindowSizeByScalingFactor(nint hWnd, int width, int height)
    {
        var dpi = User32.GetDpiForWindow(hWnd);
        float scalingFactor = (float)dpi / 96;
        width = (int)(width * scalingFactor);
        height = (int)(height * scalingFactor);
        User32.SetWindowPos(hWnd, nint.Zero, 0, 0, width, height, 2);
    }

    public static float GetScalingFactor(nint hWnd)
    {
        var dpi = User32.GetDpiForWindow(hWnd);
        float scalingFactor = (float)dpi / 96;
        return scalingFactor;
    }
}
