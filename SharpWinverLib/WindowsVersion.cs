namespace SharpWinver;

using Core;
using Constants;

public static partial class Winver
{
    public static class WindowsVersion
    {
        //获取系统版本代号
        public static string ReleaseVersionTag
        {
            get
            {
                string? osReleaseVersion = ExWinVersion.WinReleaseTag;
                if (osReleaseVersion == null) osReleaseVersion = ConstantStrings.IUnknown;
                return osReleaseVersion;
            }
        }

        //获取OS内部版本数字
        static WindowsVersion() => Load();

        private static void Load()
        {
            uint rawBuildNum = 0;
            Core.WinNTVersion.RtlGetNtVersionNumbers(ref major, ref minor, ref rawBuildNum);
            build = Core.WinNTVersion.CorrectedBuildNum(rawBuildNum);
            revision = ExWinVersion.WinUBR ?? 0;
        }

        private static uint major = 0;
        private static uint minor = 0;
        private static uint build = 0;
        private static uint revision = 0;

        public static uint Major { get => GetAndInvoke(major); }

        public static uint Minor { get => GetAndInvoke(minor); }

        public static uint Build { get => GetAndInvoke(build); }

        public static uint Revision { get => GetAndInvoke(revision); }

        public static string FullVersionTag
        {
            get => $"{Major}.{Minor}.{Build}.{Revision}";
        }

        public static string MainVersionTag
        {
            get => $"{Major}.{Minor}.{Build}";
        }

        public static string WinNTVersion
        {
            get => $"{Major}.{Minor}";
        }

        public static string WinOSVersion
        {
            get => $"{Build}.{Revision}";
        }

        public static void Reload()
        {
            if (IsInvokedOnce) Load();
        }

        private static uint GetAndInvoke(uint value)
        {
            IsInvokedOnce = true;
            return value;
        }

        private static bool IsInvokedOnce { get; set; } = false;
    }
}
