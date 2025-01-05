namespace NanoWin32Registry.Constants;

internal static class ErrorMessage
{
    public const string InvalidRegistryRoot = "Registry key name must start with a valid base key name.";
    public const string OpenRegistryFailed = "Failed to open a registry key!";
    public const string ReadValueFailed = "Error reading a registry value.";
    public const string WriteRegistryFailed = "Failed to write registry!";
    public const string RegistryOperationFailed = "A registry operation failed!";
}
