using SharpWinNTInterop;
using System.Runtime.InteropServices;

namespace SharpWinver.Core;

public static class WinNTVersion
{
    [DllImport(DLLName.NTDLL)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern void RtlGetNtVersionNumbers(out uint major, out uint minor, out uint buildraw);

    public static uint CorrectBuildNumber(uint buildraw)
        => buildraw & 0xffff;

    public static void GetWinNTVersionNumbers(out uint major, out uint minor, out uint build)
    {
        RtlGetNtVersionNumbers(out major, out minor, out uint buildraw);
        build = CorrectBuildNumber(buildraw);
    }
}
