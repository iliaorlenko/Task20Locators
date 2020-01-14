﻿using Newtonsoft.Json;
using System;
using System.IO;

namespace Task20Locators.Base
{
    public static class Settings
    {
        public static string baseDir = Directory.GetParent(Directory.GetParent(Directory.GetParent(AppContext.BaseDirectory).ToString()).ToString()).ToString();

        public static Configuration configuration = JsonConvert.DeserializeObject<Configuration>(new StreamReader(baseDir + "/Base/FrameworkConfig.json").ReadToEnd());
        public static Hub hub => configuration.Environment.Hub;
        public static Uri hubUri
        {
            get
            {
                switch (hub)
                {
                    case Hub.BrowserStack:
                        return new Uri(string.Format(configuration.BrowserStack.Uri, configuration.BrowserStack.User, configuration.BrowserStack.Key));
                    case Hub.SauceLabs:
                        return new Uri(string.Format(configuration.BrowserStack.Uri, configuration.SauceLabs.User, configuration.SauceLabs.Key));
                    case Hub.VM:
                        return new Uri(configuration.VmUri);
                    default:
                        return null;
                }
            }
        }
        public static OS os => configuration.Environment.OS;
        public static string osVersion => configuration.Environment.OsVersion;
        public static BrowserType browserName => configuration.Environment.BrowserType;
        public static string browserVersion => configuration.Environment.BrowserVersion;
        public static string tutByUrl => configuration.TutByUrl;
    }

    public class Configuration
    {
        [JsonProperty("BrowserStackSettings")]
        public BrowserStack BrowserStack { get; set; }

        [JsonProperty("SauceLabsSettings")]
        public BrowserStack SauceLabs { get; set; }

        [JsonProperty("Environment")]
        public Env Environment { get; set; }

        [JsonProperty("VmUri")]
        public string VmUri { get; set; }

        [JsonProperty("TutByUrl")]
        public string TutByUrl { get; set; }
    }

    public class Env
    {
        [JsonProperty("Hub")]
        public Hub Hub { get; set; }

        [JsonProperty("HubUrl")]
        public string HubUrl { get; set; }

        [JsonProperty("OS")]
        public OS OS { get; set; }

        [JsonProperty("OsVersion")]
        public string OsVersion { get; set; }

        [JsonProperty("BrowserType")]
        public BrowserType BrowserType { get; set; }

        [JsonProperty("BrowserVersion")]
        public string BrowserVersion { get; set; }
    }
    public class BrowserStack
    {
        [JsonProperty("User")]
        public string User { get; set; }

        [JsonProperty("Key")]
        public string Key { get; set; }

        [JsonProperty("Uri")]
        public string Uri { get; set; }
    }
    public class SauceLabs
    {
        [JsonProperty("User")]
        public string User { get; set; }

        [JsonProperty("Key")]
        public string Key { get; set; }

        [JsonProperty("Uri")]
        public string Uri { get; set; }
    }

    public enum BrowserType
    {
        Chrome,
        Edge,
        Firefox
    }

    public enum Environment
    {
        Local,
        Remote
    }

    public enum Hub
    {
        Local,
        VM,
        SauceLabs,
        BrowserStack
    }

    public enum OS
    {
        Windows,
        Linux
    }
}
