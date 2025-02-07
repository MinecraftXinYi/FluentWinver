using System.Runtime.InteropServices;

namespace SharpWinNTInterop;

public unsafe static partial class RtlStringAPI
{
    [DllImport(DLLName.NTDLL, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern int RtlInitUnicodeString(UNICODE_STRING* DestinationString, ushort* SourceString);

    [DllImport(DLLName.NTDLL, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern void RtlFreeUnicodeString(UNICODE_STRING* UnicodeString);
}
