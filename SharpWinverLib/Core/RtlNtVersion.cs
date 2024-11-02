using System.Runtime.InteropServices;

namespace SharpWinver.Core;

public static class RtlNtVersion
{
    [DllImport("ntdll.dll", SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern void RtlGetNtVersionNumbers(ref uint MajorVersion, ref uint MinorVersion, ref uint BuildNumber);

    public static uint CorrectedBuildNum(uint rawBuildNum) => rawBuildNum & 0xffff;
}
