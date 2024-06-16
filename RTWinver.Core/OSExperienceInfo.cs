namespace RTWinver
{
    using L3Services;

    public static class OSExperienceInfo
    {
        public static bool UsesCBSExperience
        {
            get
            {
                return WindowsCBSInfo.UsesCBSPackage;
            }
        }

        public static string CBSVersion
        {
            get
            {
                string cbsVersion = WindowsCBSInfo.CBSPackageVersion;
                if (cbsVersion == string.Empty) cbsVersion = "(Unknown Version)";
                return cbsVersion;
            }
        }
    }
}
