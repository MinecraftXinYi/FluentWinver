namespace SharpWinver;

using CoreExternal;

public static class WindowsUXInfo
{
    public static bool UsingClientCBSExperience
    {
        get
        {
            return WinClientCBS.IsClientCBSPackageNeeded;
        }
    }

    public const string ClientCBSPackName = "Windows Feature Experience Pack";

    public static string ClientCBSVersion
    {
        get
        {
            return WinClientCBS.ClientCBSPackageVersion;
        }
    }
}
