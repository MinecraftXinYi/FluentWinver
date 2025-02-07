using System.Runtime.InteropServices;

namespace SharpWinNTInterop;

public unsafe static partial class NTHandleAPI
{
    [DllImport(DLLName.NTDLL, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern int NtClose(HANDLE Handle);
}
