using SharpWinNTInterop.Toolsets;
using System;

namespace SharpWinver.Core;

public static class WinVersionEx
{
    public static string? GetWindowsVersionTag()
    {
        string? version = NTRegistryValueReader.GetStringValue(UsingRegistryPaths.WinNTCurrentVersion, "DisplayVersion");
        version ??= NTRegistryValueReader.GetStringValue(UsingRegistryPaths.WinNTCurrentVersion, "ReleaseId");
        version ??= NTRegistryValueReader.GetStringValue(UsingRegistryPaths.WinNTCurrentVersion, "CSDVersion");
        return version;
    }

    public static uint GetWindowsRevision()
    {
        uint? revision = NTRegistryValueReader.GetDwordValue(UsingRegistryPaths.WinNTCurrentVersion, "UBR");
        revision ??= NTRegistryValueReader.GetDwordValue(UsingRegistryPaths.WinNTCurrentVersion, "BaseBuildRevisionNumber");
        if (!revision.HasValue)
        {
            string? strCurVer = NTRegistryValueReader.GetStringValue(UsingRegistryPaths.WinNTCurrentVersion, "CurrentVersion");
            if (strCurVer != null)
            {
                if (Version.TryParse(strCurVer, out Version wCurVer))
                {
                    if (wCurVer.Revision != ConstantInts.UndefinedVersionNumber) revision = (uint)wCurVer.Revision;
                    else revision = ConstantInts.MinVersionNumber;
                }
                else revision = ConstantInts.MinVersionNumber;
            }
            else revision = ConstantInts.MinVersionNumber;
        }
        return revision.Value;
    }
}
