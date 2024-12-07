using SharpWinver;

namespace SharpWinverNativeAOTest;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("-------------------------------");
        Console.WriteLine("Test sample for SharpWinver Library on Native AOT");
        Console.WriteLine("-------------------------------");
        Console.WriteLine("----Current Windows OS Info----");
        Console.WriteLine("-------------------------------");
        Console.WriteLine("Windows Edition" + ":  " + Winver.WindowsEdition);
        Console.WriteLine("Windows Version" + ":  " + Winver.WindowsVersion.ReleaseVersionTag);
        Console.WriteLine("OS Version" + ":  " + Winver.WindowsVersion.FullVersionTag);
        Console.WriteLine("OS Architecture" + ":  " + Winver.OSArchitecture);
        Console.WriteLine("-------------------------------");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
