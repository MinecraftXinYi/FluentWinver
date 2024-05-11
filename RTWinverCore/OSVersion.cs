namespace RTWinver
{
    using Services;

    public static partial class OSVersion
    {
        //获取系统外部版本代号
        public static string DisplayVersion
        {
            get
            {
                string osDisplayVersion;
                osDisplayVersion = RegistryHelper.GetInfoString("DisplayVersion") ?? "";
                if (string.IsNullOrEmpty(osDisplayVersion))
                    osDisplayVersion = RegistryHelper.GetInfoString("ReleaseId") ?? "";
                if (string.IsNullOrEmpty(osDisplayVersion))
                    osDisplayVersion = "[Unknown]";
                return osDisplayVersion;
            }
        }

        //获取系统内部版本号各部分
        static ulong major = OSFamilyVersionService.OSFamilyVersionMajor;
        static ulong minor = OSFamilyVersionService.OSFamilyVersionMinor;
        static ulong revision = OSFamilyVersionService.OSFamilyVersionRevision;
        static ulong build = OSFamilyVersionService.OSFamilyVersionBuild;

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
