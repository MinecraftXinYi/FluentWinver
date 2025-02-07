using SharpWinver;

namespace SharpWinverNativeAOTest;

internal unsafe class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("-------------------------------");
        Console.WriteLine("Test sample for SharpWinver Library on Native AOT");
        Console.WriteLine("-------------------------------");
        Console.ReadKey();
        Console.WriteLine("----Current Windows OS Info----");
        Console.WriteLine("-------------------------------");
        Console.WriteLine("Windows Edition" + ":  " + Winver.WindowsEdition.WindowsEditionName);
        Console.WriteLine("Windows Version" + ":  " + Winver.WindowsVersion.VersionTag);
        Console.WriteLine("OS Version" + ":  " + string.Join(".", Winver.WindowsVersion.OSVersion));
        Console.WriteLine("OS Architecture" + ":  " + Winver.OSArchitecture);
        Console.WriteLine("-------------------------------");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.WriteLine(Winver.IsWindowsNT);
        Console.WriteLine(SharpWinver.Core.WinNTProcArch.GetNTOSArchitecture1().Value);
        Console.WriteLine(SharpWinver.Core.WinNTProcArch.GetNTOSArchitecture2().Value);
        Console.WriteLine(Winver.WindowsEdition.WindowsSKUName);
        Console.ReadKey();
    }
}
