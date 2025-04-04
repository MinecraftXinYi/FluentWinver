using System.Runtime.InteropServices;

namespace MinecraftXinYi.WindowsNT.Memory;

using static NTDllName;

public unsafe static partial class RtlHeapAPI
{
    [DllImport(NTDLL, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern void* RtlCreateHeap(ulong Flags, void* HeapBase, ulong ReserveSize, ulong CommitSize, void* Lock, void* Parameters);

    [DllImport(NTDLL, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern void* RtlAllocateHeap(void* HeapHandle, ulong Flags, ulong Size);

    [DllImport(NTDLL, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern LOGICAL RtlFreeHeap(void* HeapHandle, ulong Flags, void* BaseAddress);

    [DllImport(NTDLL, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern void* RtlDestroyHeap(void* HeapHandle);
}
