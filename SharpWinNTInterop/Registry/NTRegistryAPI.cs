using System.Runtime.InteropServices;

namespace SharpWinNTInterop.Registry;

public unsafe static partial class NTRegistryAPI
{
    [DllImport(DLLName.NTDLL, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern int NtOpenKey(HANDLE* KeyHandle, ulong DesiredAccess,
        OBJECT_ATTRIBUTES* ObjectAttributes);

    [DllImport(DLLName.NTDLL, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern int NtQueryValueKey(HANDLE KeyHandle, UNICODE_STRING* ValueName,
        KEY_VALUE_INFORMATION_CLASS KeyValueInformationClass, void* KeyValueInformation, ulong Length, ulong* ResultLength);
}
