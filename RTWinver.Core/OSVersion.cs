namespace RTWinver
{
    using L2Services;

    public static partial class OSVersion
    {
        //获取系统版本代号
        public static string DisplayVersion
        {
            get
            {
                string osDisplayVersion = OSVersionRaw.OSDisplayVersion;
                if (osDisplayVersion == string.Empty) osDisplayVersion = OSVersionRaw.OSReleaseId;
                if (osDisplayVersion == string.Empty) osDisplayVersion = "(Unknown)";
                return osDisplayVersion;
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
