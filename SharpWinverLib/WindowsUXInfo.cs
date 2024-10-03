namespace SharpWinver;

using CoreExternal;

public static class WindowsUXInfo
{
    public static bool UsingClientCBSExperience
    {
        get
        {
            return WinClientCBSInf.IsClientCBSPackageNeeded;
        }
    }

    public const string ClientCBSPackName = "Windows Feature Experience Pack";

    public static string ClientCBSVersion
    {
        get
        {
            return WinClientCBSInf.ClientCBSPackageVersion;
        }
    }
}
