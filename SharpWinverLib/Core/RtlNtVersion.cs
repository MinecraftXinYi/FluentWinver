using System.Runtime.InteropServices;

namespace SharpWinver.Core;

public static class RtlNtVersion
{
    [DllImport("ntdll.dll", SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern void RtlGetNtVersionNumbers(ref uint MajorVersion, ref uint MinorVersion, ref uint BuildNumber);

    public static uint CorrectedBuildNum(uint rawBuildNum) => rawBuildNum & 0xffff;

    public static class WinNTVersion
    {
        static WinNTVersion()
        {
            Load();
            LoadedOnce = true;
        }

        private static void Load()
        {
            uint major = 0;
            uint minor = 0;
            uint build = 0;
            RtlGetNtVersionNumbers(ref major, ref minor, ref build);
            Major = major;
            Minor = minor;
            Build = CorrectedBuildNum(build);
        }

        private static bool LoadedOnce = false;

        public static uint Major { get; private set; }

        public static uint Minor { get; private set; }

        public static uint Build { get; private set; }

        public static void Reload()
        {
            if (LoadedOnce) LoadedOnce = false;
            else Load();
        }
    }
}
