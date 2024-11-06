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
        Console.WriteLine("Windows Edition" + ":  " + WindowsEdition.OSEditionName);
        Console.WriteLine("Windows Version" + ":  " + WindowsVersion.ReleaseVersionTag);
        Console.WriteLine("OS Version" + ":  " + WindowsVersion.FullVersionTag);
        Console.WriteLine("OS Architecture" + ":  " + WindowsEdition.OSArchitecture);
        Console.WriteLine("-------------------------------");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
