using System.Runtime.InteropServices;

namespace MinecraftXinYi.WindowsNT;

using static NTDllName;

public unsafe static partial class NTHandleAPI
{
    [DllImport(NTDLL, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern int NtClose(HANDLE Handle);
}
