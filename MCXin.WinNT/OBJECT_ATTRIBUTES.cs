namespace MinecraftXinYi.WindowsNT;

public unsafe struct OBJECT_ATTRIBUTES
{
    public ulong Length;
    public HANDLE RootDirectory;
    public UNICODE_STRING* ObjectName;
    public ulong Attributes;
    public void* SecurityDescriptor;
    public void* SecurityQualityOfService;
}
