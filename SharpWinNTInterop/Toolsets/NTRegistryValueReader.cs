using System.Runtime.InteropServices;

namespace SharpWinNTInterop.Toolsets;

using Memory;
using Registry;

public unsafe static partial class NTRegistryValueReader
{
    private static KEY_VALUE_PARTIAL_INFORMATION* ReadKeyValuePartialInfo(string keyPath, string valueName, out void* heap)
    {
        fixed (char* key = keyPath)
        {
            void* heap_uKeyName = RtlHeapAPI.RtlCreateHeap(0, null, 0, 0, null, null);
            void* buffer_uKeyName = RtlHeapAPI.RtlAllocateHeap(heap_uKeyName, RtlHeapFlags.HEAP_ZERO_MEMORY, (ulong)sizeof(UNICODE_STRING));
            UNICODE_STRING* uKeyName = (UNICODE_STRING*)buffer_uKeyName;
            RtlStringAPI.RtlInitUnicodeString(uKeyName, (ushort*)key);
            OBJECT_ATTRIBUTES objectAttributes = new()
            {
                Length = (ulong)sizeof(OBJECT_ATTRIBUTES),
                ObjectName = uKeyName,
                Attributes = NTHandleAttributes.OBJ_CASE_INSENSITIVE,
                SecurityDescriptor = null,
                SecurityQualityOfService = null,
                RootDirectory = HANDLE.NULL
            };
            HANDLE hKey;
            NTRegistryAPI.NtOpenKey(&hKey, ACCESS_MASK.GENERIC_READ, &objectAttributes);
            fixed (char* pValue = valueName)
            {
                void* heap_uValueName = RtlHeapAPI.RtlCreateHeap(0, null, 0, 0, null, null);
                void* buffer_uValueName = RtlHeapAPI.RtlAllocateHeap(heap_uValueName, RtlHeapFlags.HEAP_ZERO_MEMORY, (ulong)sizeof(UNICODE_STRING));
                UNICODE_STRING* uValueName = (UNICODE_STRING*)buffer_uValueName;
                RtlStringAPI.RtlInitUnicodeString(uValueName, (ushort*)pValue);
                // Query once to get the size of KEY_VALUE_PARTIAL_INFORMATION we need
                ulong resultLength;
                int error = NTRegistryAPI.NtQueryValueKey(hKey, uValueName, KEY_VALUE_INFORMATION_CLASS.KeyValuePartialInformation, null, 0, &resultLength);
                if (error == -1073741772)
                {
                    // Key doesn't exist
                    NTHandleAPI.NtClose(hKey);
                    RtlHeapAPI.RtlDestroyHeap(heap_uKeyName);
                    RtlHeapAPI.RtlDestroyHeap(heap_uValueName);
                    heap = null;
                    return null;
                }
                // Second query for value.
                void* heap_partialInfo = RtlHeapAPI.RtlCreateHeap(0, null, 0, 0, null, null);
                void* buffer_partialInfo = RtlHeapAPI.RtlAllocateHeap(heap_partialInfo, RtlHeapFlags.HEAP_ZERO_MEMORY, resultLength);
                KEY_VALUE_PARTIAL_INFORMATION* partialInfo = (KEY_VALUE_PARTIAL_INFORMATION*)buffer_partialInfo;
                NTRegistryAPI.NtQueryValueKey(hKey, uValueName, KEY_VALUE_INFORMATION_CLASS.KeyValuePartialInformation, partialInfo, resultLength, &resultLength);
                NTHandleAPI.NtClose(hKey);
                RtlHeapAPI.RtlDestroyHeap(heap_uKeyName);
                RtlHeapAPI.RtlDestroyHeap(heap_uValueName);
                heap = heap_partialInfo;
                return partialInfo;
            }
        }
    }

    public static string? GetStringValue(string keyPath, string valueName)
    {
        KEY_VALUE_PARTIAL_INFORMATION* info = ReadKeyValuePartialInfo(keyPath, valueName, out void* heap);
        if (info == null) return null;
        string value = new((char*)info->Data);
        RtlHeapAPI.RtlDestroyHeap(heap);
        return value;
    }

    public static uint? GetDwordValue(string keyPath, string valueName)
    {
        KEY_VALUE_PARTIAL_INFORMATION* info = ReadKeyValuePartialInfo(keyPath, valueName, out void* heap);
        if (info == null) return null;
        uint value = (uint)Marshal.ReadInt64(new(info->Data));
        RtlHeapAPI.RtlDestroyHeap(heap);
        return value;
    }

    public static ulong? GetQwordValue(string keyPath, string valueName)
    {
        KEY_VALUE_PARTIAL_INFORMATION* info = ReadKeyValuePartialInfo(keyPath, valueName, out void* heap);
        if (info == null) return null;
        ulong value = (ulong)Marshal.ReadInt64(new(info->Data));
        RtlHeapAPI.RtlDestroyHeap(heap);
        return value;
    }
}
