using Microsoft.Win32;
using SharpWinver.Models;

namespace SharpWinver.Constants;

internal static class NativeRegPaths
{
    public static readonly RegistryPath WinNTCurrent = new()
    {
        RootHive = RegistryHive.LocalMachine,
        SubPath = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion"
    };

    public static readonly RegistryPath SystemAppxList = new()
    {
        RootHive = RegistryHive.LocalMachine,
        SubPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Appx\\AppxAllUserStore\\InboxApplications"
    };
}
