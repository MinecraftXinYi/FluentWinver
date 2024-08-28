namespace SharpWinver;

using Core;

public class WinodwsVersion
{
    //获取系统版本代号
    public static string ReleaseVersion
    {
        get
        {
            string osReleaseVersion = ExWinVersion.WinRelease;
            if (string.IsNullOrEmpty(osReleaseVersion)) osReleaseVersion = "[Unknown Release Version]";
            return osReleaseVersion;
        }
    }

    //获取OS内部版本数字
    private static uint Major { get; set; }
    private static uint Minor { get; set; }
    private static uint Build { get; set; }
    private static uint UBR { get; set; }

    static WinodwsVersion()
    {
        Major = RtlNtVersion.WinNTVersion.Major;
        Minor = RtlNtVersion.WinNTVersion.Minor;
        Build = RtlNtVersion.WinNTVersion.Build;
        UBR = ExWinVersion.WinUBR;
    }

    public static string FullVersion
    {
        get => $"{Major}.{Minor}.{Build}.{UBR}";
    }

    public static string MainVersion
    {
        get => $"{Major}.{Minor}.{Build}";
    }

    public static string NTVersion
    {
        get => $"{Major}.{Minor}";
    }

    public static string WinVersion
    {
        get => $"{Build}.{UBR}";
    }

    public static string BuildNumber
    {
        get => $"{Build}";
    }
}
