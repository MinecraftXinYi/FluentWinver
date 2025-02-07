namespace SharpWinNTInterop.RegistryAPI;

public unsafe struct KEY_VALUE_PARTIAL_INFORMATION
{
    public uint TitleIndex;
    public uint Type;
    public uint DataLength;
    public fixed byte Data[1];
}
