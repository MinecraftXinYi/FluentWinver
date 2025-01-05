using System;
using System.Runtime.InteropServices;

namespace NanoWin32Registry.Core;

using static WinMemory;

public static class WinMemory
{
    private const string KernelBaseDll = "KernelBase.dll";

    [DllImport(KernelBaseDll, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern IntPtr LocalAlloc(uint uFlags, UIntPtr sizetdwBytes);

    [DllImport(KernelBaseDll, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern IntPtr LocalFree(IntPtr handle);
}

public static class WinMemoryPackaged
{
    public static IntPtr Alloc(IntPtr cb, bool win32 = true)
    {
        UIntPtr numBytes;
        if (win32) numBytes = (UIntPtr)unchecked((uint)cb.ToInt32());
        else numBytes = (UIntPtr)unchecked((uint)cb.ToInt64());
        IntPtr pNewMem = LocalAlloc(0, unchecked(numBytes));
        if (pNewMem == IntPtr.Zero)
            Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
        return pNewMem;
    }

    public static IntPtr Alloc(int cb, bool win32 = true)
        => Alloc((IntPtr)cb, win32);

    public static void Free(IntPtr handle)
    {
        if (LocalFree(handle) != IntPtr.Zero)
            Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
    }
}
