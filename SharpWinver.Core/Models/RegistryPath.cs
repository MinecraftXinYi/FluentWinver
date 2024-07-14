using Microsoft.Win32;

namespace SharpWinver.Models;

internal class RegistryPath
{
    public RegistryHive RootHive { get; set; }
    public string SubPath { get; set; } = string.Empty;
}
