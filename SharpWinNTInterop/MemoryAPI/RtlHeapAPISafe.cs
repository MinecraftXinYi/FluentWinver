using System;
using System.Runtime.CompilerServices;

namespace SharpWinNTInterop.MemoryAPI;

using static RtlHeapAPI;

public unsafe static partial class RtlHeapAPISafe
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntPtr CreateHeap(ulong Flags, IntPtr HeapBase, ulong ReserveSize, ulong CommitSize, IntPtr Lock, IntPtr Parameters)
        => new(RtlCreateHeap(Flags, HeapBase.ToPointer(), ReserveSize, CommitSize, Lock.ToPointer(), Parameters.ToPointer()));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntPtr AllocateHeap(IntPtr HeapHandle, ulong Flags, ulong Size)
        => new(RtlAllocateHeap(HeapHandle.ToPointer(), Flags, Size));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool FreeHeap(IntPtr HeapHandle, ulong Flags, IntPtr BaseAddress)
        => (bool)RtlFreeHeap(HeapHandle.ToPointer(), Flags, BaseAddress.ToPointer());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntPtr DestroyHeap(IntPtr HeapHandle)
        => new(RtlDestroyHeap(HeapHandle.ToPointer()));
}
