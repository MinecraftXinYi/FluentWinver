using System.Runtime.InteropServices;

namespace SharpWinver.Core;

public static class RtlNtVersion
{
    [DllImport("ntdll.dll", SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern void RtlGetNtVersionNumbers(ref uint MajorVersion, ref uint MinorVersion, ref uint BuildNumber);

    public static uint FixedBuildNum(uint build) => build & 0xffff;

    public class WinNTVersion
    {
        static WinNTVersion()
        {
            uint major = 0;
            uint minor = 0;
            uint build = 0;
            RtlGetNtVersionNumbers(ref major, ref minor, ref build);
            Major = major;
            Minor = minor;
            Build = FixedBuildNum(build);
        }

        public static uint Major { get; private set; }

        public static uint Minor { get; private set; }

        public static uint Build { get; private set; }

        public static string MainVersion
        {
            get => $"{Major}.{Minor}";
        }

        public static string FullVersion
        {
            get => $"{Major}.{Minor}.{Build}";
        }
    }
}
