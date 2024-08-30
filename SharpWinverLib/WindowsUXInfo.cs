namespace SharpWinver;

using CoreExternal;

public class WindowsUXInfo
{
    public static bool HasCBSExperience
    {
        get
        {
            return WinClientCBS.UsesCBSPackage;
        }
    }

    public const string CBSPackName = "Windows Feature Experience Pack";

    public static string CBSVersion
    {
        get
        {
            return WinClientCBS.CBSPackageVersion;
        }
    }
}
