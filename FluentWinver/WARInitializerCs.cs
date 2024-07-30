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
            WASVersionInfoFromConfig wasVersionInfo = new ();
            bool result = Bootstrap.TryInitialize
                (wasVersionInfo.MajorMinorVersion, wasVersionInfo.VersionTag, wasVersionInfo.MinVersion,
                Bootstrap.InitializeOptions.OnNoMatch_ShowUI, out hResult);
            return result;
        }
        catch (Exception ex)
        {
            hResult = ex.HResult;
            return false;
        }
    }

    public class WASVersionInfoFromConfig
    {
#pragma warning disable CA1507
        private readonly JsonElement config;
        private readonly JsonDocumentOptions jsonDocumentOptions = new ()
        {
            AllowTrailingCommas = true,
            CommentHandling = JsonCommentHandling.Skip,
        };

        public WASVersionInfoFromConfig()
        {
            string configString = File.ReadAllText("WASDKVersionInfo.json");
            JsonDocument configDoc = JsonDocument.Parse(configString, jsonDocumentOptions);
            config = configDoc.RootElement.GetProperty("WASDKVersion");
        }

        public uint MajorMinorVersion
        {
            get
            {
                string rawdata = config.GetProperty("MajorMinorVersion").GetString();
                return Convert.ToUInt32(rawdata, 16);
            }
        }

        public PackageVersion MinVersion
        {
            get
            {
                string rawdata = config.GetProperty("MinVersion").GetString();
                return new PackageVersion(Convert.ToUInt64(rawdata, 16));
            }
        }

        public string VersionTag
        {
            get
            {
                return config.GetProperty("VersionTag").GetString();
            }
        }
    }
}
