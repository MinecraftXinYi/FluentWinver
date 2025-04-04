using MinecraftXinYi.WindowsNT;
using System;
using System.Runtime.InteropServices;

namespace SharpWinver.Core;

public unsafe static class WinNTProcArch
{
    public enum ProcessorArchitecture : ushort
    {
        PROCESSOR_ARCHITECTURE_AMD64 = 9,
        PROCESSOR_ARCHITECTURE_ARM = 5,
        PROCESSOR_ARCHITECTURE_ARM64 = 12,
        PROCESSOR_ARCHITECTURE_IA64 = 6,
        PROCESSOR_ARCHITECTURE_INTEL = 0,
        PROCESSOR_ARCHITECTURE_UNKNOWN = 0xffff
    }

    public static Architecture? ArchitectureMap1(ushort processorArchitecture)
    {
        if (!ProcessorArchitecture.TryParse(processorArchitecture.ToString(), out ProcessorArchitecture parch)) return null;
        Architecture? architecture = parch switch
        {
            ProcessorArchitecture.PROCESSOR_ARCHITECTURE_AMD64 => Architecture.X64,
            ProcessorArchitecture.PROCESSOR_ARCHITECTURE_IA64 => Architecture.X64,
            ProcessorArchitecture.PROCESSOR_ARCHITECTURE_INTEL => Architecture.X86,
            ProcessorArchitecture.PROCESSOR_ARCHITECTURE_ARM64 => Architecture.Arm64,
            ProcessorArchitecture.PROCESSOR_ARCHITECTURE_ARM => Architecture.Arm,
            ProcessorArchitecture.PROCESSOR_ARCHITECTURE_UNKNOWN => null,
            _ => null
        };
        return architecture;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct SYSTEM_INFO
    {
        internal ushort wProcessorArchitecture;
        internal ushort wReserved;
        internal int dwPageSize;
        internal IntPtr lpMinimumApplicationAddress;
        internal IntPtr lpMaximumApplicationAddress;
        internal IntPtr dwActiveProcessorMask;
        internal int dwNumberOfProcessors;
        internal int dwProcessorType;
        internal int dwAllocationGranularity;
        internal short wProcessorLevel;
        internal short wProcessorRevision;
    }

    private static bool TryGetNativeSystemInfo(SYSTEM_INFO* lpSystemInfo)
    {
        try
        {
            [DllImport(DllName.KernelBase)]
            [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
            static extern void GetNativeSystemInfo(SYSTEM_INFO* lpSystemInfo);
            GetNativeSystemInfo(lpSystemInfo);
            return true;
        }
        catch (Exception)
        {
            try
            {
                [DllImport(DllName.Kernel32)]
                [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
                static extern void GetNativeSystemInfo(SYSTEM_INFO* lpSystemInfo);
                GetNativeSystemInfo(lpSystemInfo);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public static Architecture? GetNTOSArchitecture1()
    {
        SYSTEM_INFO systemInfo;
        if (!TryGetNativeSystemInfo(&systemInfo)) return null;
        return ArchitectureMap1(systemInfo.wProcessorArchitecture);
    }

    public enum IMAGE_FILE_MACHINE_PARTIAL : ushort
    {
        IMAGE_FILE_MACHINE_UNKNOWN = 0,
        IMAGE_FILE_MACHINE_I386 = 0x014c,
        IMAGE_FILE_MACHINE_ARM = 0x01c0,
        IMAGE_FILE_MACHINE_THUMB = 0x01c2,
        IMAGE_FILE_MACHINE_ARMNT = 0x01c4,
        IMAGE_FILE_MACHINE_IA64 = 0x0200,
        IMAGE_FILE_MACHINE_AMD64 = 0x8664,
        IMAGE_FILE_MACHINE_ARM64 = 0xAA64
    }

    public static Architecture? ArchitectureMap2(ushort IMAGE_FILE_MACHINE)
    {
        if (!IMAGE_FILE_MACHINE_PARTIAL.TryParse(IMAGE_FILE_MACHINE.ToString(), out IMAGE_FILE_MACHINE_PARTIAL march)) return null;
        Architecture? architecture = march switch
        {
            IMAGE_FILE_MACHINE_PARTIAL.IMAGE_FILE_MACHINE_I386 => Architecture.X86,
            IMAGE_FILE_MACHINE_PARTIAL.IMAGE_FILE_MACHINE_AMD64 => Architecture.X64,
            IMAGE_FILE_MACHINE_PARTIAL.IMAGE_FILE_MACHINE_IA64 => Architecture.X64,
            IMAGE_FILE_MACHINE_PARTIAL.IMAGE_FILE_MACHINE_ARM => Architecture.Arm,
            IMAGE_FILE_MACHINE_PARTIAL.IMAGE_FILE_MACHINE_THUMB => Architecture.Arm,
            IMAGE_FILE_MACHINE_PARTIAL.IMAGE_FILE_MACHINE_ARMNT => Architecture.Arm,
            IMAGE_FILE_MACHINE_PARTIAL.IMAGE_FILE_MACHINE_ARM64 => Architecture.Arm64,
            IMAGE_FILE_MACHINE_PARTIAL.IMAGE_FILE_MACHINE_UNKNOWN => null,
            _ => null
        };
        return architecture;
    }

    private static HANDLE GetCurrentProcessManaged()
    {
        try
        {
            [DllImport(DllName.KernelBase)]
            [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
            static extern HANDLE GetCurrentProcess();
            return GetCurrentProcess();
        }
        catch (Exception)
        {
            try
            {
                [DllImport(DllName.Kernel32)]
                [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
                static extern HANDLE GetCurrentProcess();
                return GetCurrentProcess();
            }
            catch (Exception)
            {
                return new((void*)-1);
            }
        }
    }

    private static bool TryGetImageFileMachine(HANDLE hProcess, ushort* pProcessMachine, ushort* pNativeMachine)
    {
        try
        {
            [DllImport(DllName.KernelBase, SetLastError = true)]
            [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
            static extern BOOL IsWow64Process2(HANDLE hProcess, ushort* pProcessMachine, ushort* pNativeMachine);
            IsWow64Process2(hProcess, pProcessMachine, pNativeMachine);
            return true;
        }
        catch (Exception)
        {
            try
            {
                [DllImport(DllName.Kernel32, SetLastError = true)]
                [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
                static extern BOOL IsWow64Process2(HANDLE hProcess, ushort* pProcessMachine, ushort* pNativeMachine);
                IsWow64Process2(hProcess, pProcessMachine, pNativeMachine);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public static Architecture? GetNTOSArchitecture2()
    {
        HANDLE hProcess = GetCurrentProcessManaged();
        ushort processMachine, nativeMachine;
        if (!TryGetImageFileMachine(hProcess, &processMachine, &nativeMachine)) return null;
        return ArchitectureMap2(nativeMachine);
    }
}
