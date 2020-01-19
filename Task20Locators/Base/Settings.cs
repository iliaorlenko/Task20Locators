using Newtonsoft.Json;
using System;
using System.IO;

namespace Task20Locators.Base
{
    public static class Settings
    {
        // Path to bin/Debug folder
        public static string baseDir = Directory.GetParent(Directory.GetParent(Directory.GetParent(AppContext.BaseDirectory).ToString()).ToString()).ToString();

        // Deserialized FrameworkConfig.json
        private static Configuration configuration = JsonConvert.DeserializeObject<Configuration>(new StreamReader(baseDir + "/Base/FrameworkConfig.json").ReadToEnd());

        public static string tutByMainPage => configuration.TutByUrl;
        public static string chromePort => configuration.ChromePort;
        public static string firefoxPort => configuration.FirefoxPort;

        public static Environment? env = null;
        public static Uri hubUri
        {
            get
            {
                switch (env)
                {
                    case Base.Environment.BrowserStack:
                        return new Uri(string.Format(configuration.BrowserStack.Uri, configuration.BrowserStack.User, configuration.BrowserStack.Key));
                    case Base.Environment.SauceLabs:
                        return new Uri(string.Format(configuration.SauceLabs.Uri, configuration.SauceLabs.User, configuration.SauceLabs.Key));
                    case Base.Environment.VM:
                        return new Uri(configuration.VmUri);
                    default:
                        return null;
                }
            }
        }

        private class Configuration
        {
            [JsonProperty("BrowserStackSettings")]
            public BrowserStack BrowserStack { get; set; }

            [JsonProperty("SauceLabsSettings")]
            public SauceLabs SauceLabs { get; set; }

            [JsonProperty("VmUri")]
            public string VmUri { get; set; }

            [JsonProperty("ChromePort")]
            public string ChromePort { get; set; }

            [JsonProperty("FirefoxPort")]
            public string FirefoxPort { get; set; }

            [JsonProperty("TutByUrl")]
            public string TutByUrl { get; set; }
        }

        private class BrowserStack
        {
            [JsonProperty("User")]
            public string User { get; set; }

            [JsonProperty("Key")]
            public string Key { get; set; }

            [JsonProperty("Uri")]
            public string Uri { get; set; }
        }
        private class SauceLabs
        {
            [JsonProperty("User")]
            public string User { get; set; }

            [JsonProperty("Key")]
            public string Key { get; set; }

            [JsonProperty("Uri")]
            public string Uri { get; set; }
        }
    }
    public enum BrowserName
    {
        Chrome,
        Edge,
        Firefox,
        IE,
        Safari
    }

    public enum Environment
    {
        Local,
        VM,
        SauceLabs,
        BrowserStack
    }

    public enum OS
    {
        Android,
        Ios,
        Linux,
        Mac,
        Windows
    }
}
