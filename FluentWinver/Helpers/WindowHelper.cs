using System;
using System.Runtime.InteropServices;

namespace Win32WindowHelper
{
    //Native Classes
    public static class Win32WindowAPI
    {
        public static void SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags)
        {
            [DllImport("user32.dll", SetLastError = true)]
            static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);
            SetWindowPos(hWnd, hWndInsertAfter, X, Y, cx, cy, uFlags);
        }

        public static uint GetDpiForWindow(IntPtr hWnd)
        {
            [DllImport("user32.dll", SetLastError = true)]
            static extern uint GetDpiForWindow(IntPtr hWnd);
            return GetDpiForWindow(hWnd);
        }
    }

    //Packaged Classes
    public static class Win32Windowing
    {
        public static void SetWindowSizeByScalingFactor(IntPtr hWnd, int width, int height)
        {
            var dpi = Win32WindowAPI.GetDpiForWindow(hWnd);
            float scalingFactor = (float)dpi / 96;
            width = (int)(width * scalingFactor);
            height = (int)(height * scalingFactor);

            Win32WindowAPI.SetWindowPos(hWnd, System.IntPtr.Zero, 0, 0, width, height, 2);
        }

        public static float GetScalingFactor(IntPtr hWnd)
        {
            var dpi = Win32WindowAPI.GetDpiForWindow(hWnd);
            float scalingFactor = (float)dpi / 96;
            return scalingFactor;
        }
    }
}
