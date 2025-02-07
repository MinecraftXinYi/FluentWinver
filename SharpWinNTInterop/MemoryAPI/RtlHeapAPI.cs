using System.Runtime.InteropServices;

namespace SharpWinNTInterop.MemoryAPI;

public unsafe static partial class RtlHeapAPI
{
    [DllImport(DLLName.NTDLL, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern void* RtlCreateHeap(ulong Flags, void* HeapBase, ulong ReserveSize, ulong CommitSize, void* Lock, void* Parameters);

    [DllImport(DLLName.NTDLL, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern void* RtlAllocateHeap(void* HeapHandle, ulong Flags, ulong Size);

    [DllImport(DLLName.NTDLL, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern LOGICAL RtlFreeHeap(void* HeapHandle, ulong Flags, void* BaseAddress);

    [DllImport(DLLName.NTDLL, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern void* RtlDestroyHeap(void* HeapHandle);
}
