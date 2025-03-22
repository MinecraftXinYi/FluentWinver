using System.IO;
using System.Text.Json;

namespace FluentWinver.Helpers;

internal static class MsixRTListHelperUnsafe
{
    public static JsonElement GetMsixRuntimeInfoItem(string configFileName, string runtimeName)
    {
        string configString = File.ReadAllText(configFileName);
        JsonDocument configDocument = JsonDocument.Parse(configString, jsonDocumentOptions);
        JsonElement configElement = configDocument.RootElement.GetProperty("MSIXRuntimeInfo");
        return configElement.GetProperty(runtimeName).Clone();
    }

    private static readonly JsonDocumentOptions jsonDocumentOptions = new()
    {
        AllowTrailingCommas = true,
        CommentHandling = JsonCommentHandling.Skip
    };

    public static JsonElement MajorMinorVersion(JsonElement runtimeInfoItemEntity)
    {
        return runtimeInfoItemEntity.GetProperty("MajorMinorVersion");
    }

    public static JsonElement MinVersion(JsonElement runtimeInfoItemEntity)
    {
        return runtimeInfoItemEntity.GetProperty("MinVersion");
    }

    public static JsonElement VersionTag(JsonElement runtimeInfoItemEntity)
    {
        return runtimeInfoItemEntity.GetProperty("VersionTag");
    }
}
