namespace SharpWinNTInterop;

public unsafe struct UNICODE_STRING
{
    public ushort Length;
    public ushort MaximumLength;
    public ushort* Buffer;
}
