namespace SharpWinver;

using CoreExternal;

public class WindowsUXInfo
{
    public static bool HasCBSExperience
    {
        get
        {
            return WindowsCBS.UsesCBSPackage;
        }
    }

    public const string CBSPackName = "Windows Feature Experience Pack";

    public static string CBSVersion
    {
        get
        {
            return WindowsCBS.CBSPackageVersion;
        }
    }
}
