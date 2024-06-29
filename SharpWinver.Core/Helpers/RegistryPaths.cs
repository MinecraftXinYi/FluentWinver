using Microsoft.Win32;

namespace SharpWinver.Helpers;

internal static class RegistryPaths
{
    public static readonly RegistryPath WinNTCurrent = new ()
    {
        RootHive = RegistryHive.LocalMachine,
        SubPath = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion"
    };

    public static readonly RegistryPath SystemAppxList = new ()
    {
        RootHive = RegistryHive.LocalMachine,
        SubPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\InboxApplications"
    };
}
