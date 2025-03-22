//SharpWinver Library
//This is a .NET Standard library that provides version information of Windows OS via common CSharp methods.
//Author: Github/MinecraftXinYi
//Version: 2.1.1

using SharpWinNTInterop;
using System;
using System.Runtime.InteropServices;

namespace SharpWinver;

/// <summary>
/// 获取正在运行的 Windows 系统副本的版本信息
/// </summary>
public static partial class Winver
{
    /// <summary>
    /// 检测当前运行的操作系统是否基于 Windows NT 内核
    /// (只有基于或模拟 Windows NT 内核的操作系统才能正常调用该类中的方法)
    /// </summary>
    public static bool IsWindowsNT
    {
        get
        {
            try
            {
                [DllImport(DLLName.NTDLL, CallingConvention = CallingConvention.StdCall, SetLastError = false)]
                [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
                static extern double sin(double x);
                sin(0);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
