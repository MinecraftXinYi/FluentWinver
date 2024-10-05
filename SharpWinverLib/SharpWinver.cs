//Winver info loader Core Library based on WinRT api & .NET Assembly
//Version: 2.0.0

namespace SharpWinver;

using Core;
using Helpers;

public static class SharpWinverNative
{
    public static void ReInitialize()
    {
        RtlNtVersion.WinNTVersion.Reload();
        UsingRegistryKeys.Initialize();
        WindowsVersion.Reload();
    }
}
