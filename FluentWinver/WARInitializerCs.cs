using System;
using System.IO;
using System.Text.Json;

namespace Microsoft.Windows.ApplicationModel.DynamicDependency;

internal static class WARInitializerCs
{
    public static bool InitializeWAR(out int hResult)
    {
        try
        {
            bool result = Bootstrap.TryInitialize
                (WASVersionInfoConfig.MajorMinorVersion, WASVersionInfoConfig.VersionTag, WASVersionInfoConfig.MinVersion,
                Bootstrap.InitializeOptions.OnNoMatch_ShowUI, out hResult);
            return result;
        }
        catch (Exception ex)
        {
            hResult = ex.HResult;
            return false;
        }
    }

    public static class WASVersionInfoConfig
    {
#pragma warning disable CA1507
        private static readonly JsonElement config;

        private static readonly JsonDocumentOptions jsonDocumentOptions = new ()
        {
            AllowTrailingCommas = true,
            CommentHandling = JsonCommentHandling.Skip,
        };

        static WASVersionInfoConfig()
        {
            string configString = File.ReadAllText("WASDKVersionInfo.json");
            JsonDocument configDoc = JsonDocument.Parse(configString, jsonDocumentOptions);
            config = configDoc.RootElement.GetProperty("WASDKVersion");
        }

        public static uint MajorMinorVersion
        {
            get
            {
                string rawdata = config.GetProperty("MajorMinorVersion").GetString();
                return Convert.ToUInt32(rawdata, 16);
            }
        }

        public static PackageVersion MinVersion
        {
            get
            {
                string rawdata = config.GetProperty("MinVersion").GetString();
                return new PackageVersion(Convert.ToUInt64(rawdata, 16));
            }
        }

        public static string VersionTag
        {
            get
            {
                return config.GetProperty("VersionTag").GetString();
            }
        }
    }
}
