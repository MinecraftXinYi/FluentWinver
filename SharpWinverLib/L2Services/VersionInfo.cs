using SharpWinver.Helpers;
using SharpWinver.Models.Constants;
using SharpWinver.Services;
using System;

namespace SharpWinver.L2Services;

internal static class VersionInfo
{
    public static string OSDisplayVersion
    {
        get
        {
            RegistryHelper.TryGetRegString(RegistryPaths.WinNTCurrent, "DisplayVersion", out string osDisplayVersion);
            return osDisplayVersion;
        }
    }

    public static string OSReleaseId
    {
        get
        {
            RegistryHelper.TryGetRegString(RegistryPaths.WinNTCurrent, "ReleaseId", out string osReleaseId);
            return osReleaseId;
        }
    }

    public static ulong OSVersionMajor
    {
        get
        {
            try
            {
                return OSFamilyVersion.OSFamilyVersionMajor;
            }
            catch (Exception)
            {
                return RegistryOSVersion.OSVersionMajor;
            }
        }
    }

    public static ulong OSVersionMinor
    {
        get
        {
            try
            {
                return OSFamilyVersion.OSFamilyVersionMinor;
            }
            catch (Exception)
            {
                return RegistryOSVersion.OSVersionMinor;
            }
        }
    }

    public static ulong OSVersionRevision
    {
        get
        {
            try
            {
                return OSFamilyVersion.OSFamilyVersionRevision;
            }
            catch (Exception)
            {
                return RegistryOSVersion.OSVersionRevision;
            }
        }
    }

    public static ulong OSVersionBuild
    {
        get
        {
            try
            {
                return OSFamilyVersion.OSFamilyVersionBuild;
            }
            catch (Exception)
            {
                return RegistryOSVersion.OSVersionBuild;
            }
        }
    }
}
