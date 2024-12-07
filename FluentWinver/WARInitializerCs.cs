using System;
using System.IO;
using System.Text.Json;
using System.Runtime.InteropServices;

namespace Microsoft.Windows.ApplicationModel.DynamicDependency;

internal static class WARInitializerCs
{
    public static bool InitializeWAR(out int hResult)
    {
        try
        {
            if (!WARVersionConfig.Loaded) WARVersionConfig.Load();
            bool result = Bootstrap.TryInitialize
                (WARVersionConfig.MajorMinorVersion, WARVersionConfig.VersionTag, WARVersionConfig.MinVersion,
                Bootstrap.InitializeOptions.OnNoMatch_ShowUI, out hResult);
            return result;
        }
        catch (Exception ex)
        {
            hResult = ex.HResult;
            return false;
        }
    }

    public static class WARVersionConfig
    {
#pragma warning disable CA1507
        private static JsonElement config;

        private static readonly JsonDocumentOptions jsonDocumentOptions = new ()
        {
            AllowTrailingCommas = true,
            CommentHandling = JsonCommentHandling.Skip,
        };

        public static void Load()
        {
            string configString = File.ReadAllText("MSIXRuntimeInfo.json");
            JsonDocument configDoc = JsonDocument.Parse(configString, jsonDocumentOptions);
            config = configDoc.RootElement.GetProperty("MSIXRuntimeInfo").GetProperty("Microsoft.WindowsAppRuntime");
            Loaded = true;
        }

        public static bool Loaded { get; private set; } = false;

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

    [DllImport("Microsoft.ui.xaml.dll")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    public static extern void XamlCheckProcessRequirements();
}
