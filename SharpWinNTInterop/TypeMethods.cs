using System.Runtime.CompilerServices;

namespace SharpWinNTInterop;

public unsafe static partial class TypeMethods
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CompareTo(this nuint left, nuint right)
    {
        if (sizeof(nuint) == sizeof(uint))
        {
            return ((uint)left).CompareTo((uint)right);
        }

        return ((ulong)left).CompareTo(right);
    }

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
