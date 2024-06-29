namespace SharpWinver
{
    using L2Services;

    public static partial class OSVersion
    {
        //获取系统版本代号
        public static string ReleaseVersion
        {
            get
            {
                string osReleaseVersion = OSVersionRaw.OSDisplayVersion;
                if (osReleaseVersion == string.Empty) osReleaseVersion = OSVersionRaw.OSReleaseId;
                if (osReleaseVersion == string.Empty) osReleaseVersion = "(Unknown)";
                return osReleaseVersion;
            }
        }

        //获取OS内部版本数字
        static readonly ulong major = OSVersionRaw.OSVersionMajor;
        static readonly ulong minor = OSVersionRaw.OSVersionMinor;
        static readonly ulong revision = OSVersionRaw.OSVersionRevision;
        static readonly ulong build = OSVersionRaw.OSVersionBuild;

        public static string FullVersion
        {
            get
            {
                return $"{major}.{minor}.{revision}.{build}";
            }
        }

        public static string MainVersion
        {
            get
            {
                return $"{major}.{minor}.{revision}";
            }
        }

        public static string NTVersion
        {
            get
            {
                return $"{major}.{minor}";
            }
        }

        public static string WinVersion
        {
            get
            {
                return $"{revision}.{build}";
            }
        }

        public static string Revision
        {
            get
            {
                return $"{revision}";
            }
        }
    }
}
