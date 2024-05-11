using static WinverUWP.Interop.Windows;

namespace RTWinver.Services;

internal unsafe static class WinbrandService
{
    public static string GetWinbrand()
    {
        fixed(char* pModuleName = "Winbrand.dll")
        {
            HMODULE module = LoadLibraryW((ushort*)pModuleName);
            fixed(byte* pProc = "BrandingFormatString"u8)
            {
                var func = (delegate* unmanaged[Stdcall]<ushort*, ushort*>)GetProcAddress(module, (sbyte*)pProc);
                fixed (char* pFormatName = "%WINDOWS_LONG%")
                {
                    ushort* branding = func((ushort*)pFormatName);
                    FreeLibrary(module);
                    return new string((char*)branding);
                }
            }
        }
    }
}
