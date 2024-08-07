﻿namespace SharpWinver;

using L3Services;

public static class OSExperienceInfo
{
    public static bool UseCBSExperience
    {
        get
        {
            return WindowsCBSInfo.UseCBSPackage;
        }
    }

    public static readonly string CBSPackName = "Windows Feature Experience Pack";

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
