using System;
using System.Runtime.CompilerServices;

namespace MinecraftXinYi.WindowsNT.Memory;

using static RtlHeapAPI;

public unsafe static partial class RtlHeapManaged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntPtr Create(ulong Flags, IntPtr HeapBase, ulong ReserveSize, ulong CommitSize, IntPtr Lock, IntPtr Parameters)
        => new(RtlCreateHeap(Flags, HeapBase.ToPointer(), ReserveSize, CommitSize, Lock.ToPointer(), Parameters.ToPointer()));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntPtr Allocate(IntPtr HeapHandle, ulong Flags, ulong Size)
        => new(RtlAllocateHeap(HeapHandle.ToPointer(), Flags, Size));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Free(IntPtr HeapHandle, ulong Flags, IntPtr BaseAddress)
        => (bool)RtlFreeHeap(HeapHandle.ToPointer(), Flags, BaseAddress.ToPointer());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntPtr Destroy(IntPtr HeapHandle)
        => new(RtlDestroyHeap(HeapHandle.ToPointer()));
}
