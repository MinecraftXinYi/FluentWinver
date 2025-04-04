using System.Runtime.CompilerServices;

namespace MinecraftXinYi.WindowsNT;

public unsafe static partial class NTObjectHelper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void InitializeObjectAttributes(OBJECT_ATTRIBUTES* p, UNICODE_STRING* n, ulong a, HANDLE r, void* s)
    {
        p->Length = (ulong)sizeof(OBJECT_ATTRIBUTES);
        p->RootDirectory = r;
        p->Attributes = a;
        p->ObjectName = n;
        p->SecurityDescriptor = s;
        p->SecurityQualityOfService = null;
    }
}
