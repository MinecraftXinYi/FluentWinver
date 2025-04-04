using System.Runtime.InteropServices;

namespace MinecraftXinYi.WindowsNT;

using static NTDllName;

public unsafe static partial class RtlStringAPI
{
    [DllImport(NTDLL, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern int RtlInitUnicodeString(UNICODE_STRING* DestinationString, ushort* SourceString);

    [DllImport(NTDLL, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern void RtlFreeUnicodeString(UNICODE_STRING* UnicodeString);
}
