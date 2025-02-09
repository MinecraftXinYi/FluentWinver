using SharpWinNTInterop.Toolsets;
using System;

namespace SharpWinver.Core;

public static class WinVersionEx
{
    public static string? GetWindowsVersionTag()
    {
        string? version = NTRegistryValueReader.GetStringValue(ConstRegistryPaths.WinNTCurrentVersion, "DisplayVersion");
        version ??= NTRegistryValueReader.GetStringValue(ConstRegistryPaths.WinNTCurrentVersion, "ReleaseId");
        version ??= NTRegistryValueReader.GetStringValue(ConstRegistryPaths.WinNTCurrentVersion, "CSDVersion");
        return version;
    }

    public static uint GetWindowsRevision()
    {
        uint? revision = NTRegistryValueReader.GetDwordValue(ConstRegistryPaths.WinNTCurrentVersion, "UBR");
        revision ??= NTRegistryValueReader.GetDwordValue(ConstRegistryPaths.WinNTCurrentVersion, "BaseBuildRevisionNumber");
        if (!revision.HasValue)
        {
            string? strCurVer = NTRegistryValueReader.GetStringValue(ConstRegistryPaths.WinNTCurrentVersion, "CurrentVersion");
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
