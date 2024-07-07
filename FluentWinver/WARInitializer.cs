using Microsoft.Windows.ApplicationModel.DynamicDependency;
using System;
using System.IO;
using System.Text.Json;

internal static class WARInitializer
{
    public static bool InitializeWAR(out int HResult)
    {
        uint? majorMinorVersion = WASVersionInfoFromConfig.MajorMinorVersion;
        PackageVersion? minVersion = WASVersionInfoFromConfig.MinVersion;
        string versionTag = WASVersionInfoFromConfig.VersionTag;
        if (majorMinorVersion.HasValue && minVersion.HasValue)
        {
            try
            {
                bool result = Bootstrap.TryInitialize(majorMinorVersion.Value, versionTag, minVersion.Value,
                    NoMatchAndShowUIOption, out HResult);
                return result;
            }
            catch (Exception ex)
            {
                HResult = ex.HResult;
                return false;
            }
        }
        else
        {
            HResult = 1;
            return false;
        }
    }

    public static Bootstrap.InitializeOptions NoMatchAndShowUIOption
    {
        get => Bootstrap.InitializeOptions.OnNoMatch_ShowUI;
    }

    public static class WASVersionInfoFromConfig
    {
        static JsonElement? verconfig;
        static readonly JsonDocumentOptions JsonDocumentOptions = new()
        {
            AllowTrailingCommas = true,
            CommentHandling = JsonCommentHandling.Skip,
        };
        static void GetConfig()
        {
            try
            {
                string configString = File.ReadAllText("WASDKVersionInfo.json");
                JsonDocument configDoc = JsonDocument.Parse(configString, JsonDocumentOptions);
                verconfig = configDoc.RootElement.GetProperty("WASDKVersionInfo");
            }
            catch (Exception)
            {
                verconfig = null;
            }
        }

        public static uint? MajorMinorVersion
        {
            get
            {
                if (!verconfig.HasValue) GetConfig();
                try
                {
                    if (verconfig.HasValue)
                    {
                        string rawdata = verconfig.Value.GetProperty("MajorMinorVersion").GetString();
                        return Convert.ToUInt32(rawdata, 16);
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static PackageVersion? MinVersion
        {
            get
            {
                if (!verconfig.HasValue) GetConfig();
                try
                {
                    if (verconfig.HasValue)
                    {
                        string rawdata = verconfig.Value.GetProperty("MinVersion").GetString();
                        return new PackageVersion(Convert.ToUInt64(rawdata, 16));
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static string VersionTag
        {
            get
            {
                if (!verconfig.HasValue) GetConfig();
                try
                {
                    if (verconfig.HasValue)
                    {
                        return verconfig.Value.GetProperty("VersionTag").GetString();
                    }
                    else
                    {
                        return "";
                    }
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }
    }
}
