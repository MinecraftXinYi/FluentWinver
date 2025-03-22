using System;

namespace SharpWinver.Core;

internal static class UnixDateTime
{
    public static readonly DateTime MinValue = (new DateTime(1970, 1, 1)).ToUniversalTime();

    public static DateTime FromUnixTimestamp(double timestamp)
        => MinValue.AddSeconds(timestamp);

    public static bool TryConvertFromUnixTimestamp(double timestamp, out DateTime convertedDateTime)
    {
        try
        {
            convertedDateTime = FromUnixTimestamp(timestamp);
            return true;
        }
        catch (Exception)
        {
            convertedDateTime = MinValue;
            return false;
        }
    }
}
