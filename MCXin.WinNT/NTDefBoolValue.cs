namespace MinecraftXinYi.WindowsNT;

public static class NTDefBoolValue
{
    public const byte
        FALSE = 0,
        TRUE = 1;

    public static byte FromBoolValue(bool value)
        => value ? TRUE : FALSE;

    public static bool ToBoolValue(ulong value)
        => value switch
        {
            FALSE => false,
            TRUE => true,
            _ => true
        };
}
