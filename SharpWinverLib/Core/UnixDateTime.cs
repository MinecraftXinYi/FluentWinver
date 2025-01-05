using System;

namespace SharpWinver.Core;

internal static class UnixDateTime
{
    public static readonly DateTime MinValue = (new DateTime(1970, 1, 1)).ToUniversalTime();

    public static DateTime FromUnixTimeSeconds(double unixTimeSeconds)
        => MinValue.AddSeconds(unixTimeSeconds).ToUniversalTime();

    public static bool TryConvertFromUnixTimeSeconds(double unixTimeSeconds, out DateTime? convertedDateTime)
    {
        try
        {
            convertedDateTime = FromUnixTimeSeconds(unixTimeSeconds);
            return true;
        }
        catch (Exception)
        {
            convertedDateTime = null;
            return false;
        }
    }
}
