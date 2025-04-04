namespace MinecraftXinYi.WindowsNT.Memory;

public static class RtlHeapFlags
{
    public const ulong
    HEAP_NO_SERIALIZE = 0x00000001,
    HEAP_GROWABLE = 0x00000002,
    HEAP_GENERATE_EXCEPTIONS = 0x00000004,
    HEAP_ZERO_MEMORY = 0x00000008;
}
