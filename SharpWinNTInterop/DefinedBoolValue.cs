namespace SharpWinNTInterop;

internal enum DefinedBoolValue : byte
{
    FALSE,
    DefaultTRUE
}

internal static class DefinedBoolValueHelper
{
    internal static bool IsTrue(ulong boolValue)
        => boolValue >= (byte)DefinedBoolValue.DefaultTRUE;

    internal static bool IsFalse(ulong boolValue)
        => boolValue == (byte)DefinedBoolValue.FALSE;
}
