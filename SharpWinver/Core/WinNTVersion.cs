using MinecraftXinYi.WindowsNT;
using System.Runtime.InteropServices;

namespace SharpWinver.Core;

public static class WinNTVersion
{
    [DllImport(NTDllName.NTDLL)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern void RtlGetNtVersionNumbers(out uint major, out uint minor, out uint buildraw);

    public static uint CorrectBuildNumber(uint buildraw)
        => buildraw & 0xffff;

    public static uint[] GetWinNTVersionNumbers()
    {
        RtlGetNtVersionNumbers(out uint major, out uint minor, out uint buildraw);
        uint build = CorrectBuildNumber(buildraw);
        return new uint[] { major, minor, build };
    }
}
