using SharpWinNTInterop;
using System.Runtime.InteropServices;

namespace SharpWinver.Core;

public static class WinNTVersion
{
    [DllImport(DLLName.NTDLL, SetLastError = false)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern void RtlGetNtVersionNumbers(ref uint MajorVersion, ref uint MinorVersion, ref uint BuildNumber);

    public static uint CorrectBuildNumber(uint BuildNumber) => BuildNumber & 0xffff;

    public static uint[] GetNtVersion()
    {
        uint major = ConstantInts.MinVersionNumber;
        uint minor = ConstantInts.MinVersionNumber;
        uint buildraw = ConstantInts.MinVersionNumber;
        RtlGetNtVersionNumbers(ref major, ref minor, ref buildraw);
        uint build = CorrectBuildNumber(buildraw);
        return new uint[] { major, minor, build };
    }
}
