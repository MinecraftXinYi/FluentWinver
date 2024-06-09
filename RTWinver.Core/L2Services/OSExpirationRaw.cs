using RTWinver.Services;
using System;

namespace RTWinver.L2Services;

internal static class OSExpirationRaw
{
    public static bool HasExpiration
    {
        get => ExpirationInfo.GetSystemExpiration().HasValue;
    }

    public static DateTime? ExpirationTime
    {
        get => ExpirationInfo.GetSystemExpiration();
    }
}

