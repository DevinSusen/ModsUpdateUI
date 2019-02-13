using Newtonsoft.Json;
using System;

namespace ModsUpdateUI.Configurations
{
    public class SoftwareInfo
    {
        [JsonProperty("Author")]
        public string Author { get; set; }

        [JsonProperty("HomePage")]
        public Uri HomePage { get; set; }

        [JsonProperty("Version")]
        public string Version { get; set; }

        [JsonProperty("ReleaseTime")]
        public DateTime ReleaseTime { get; set; }

        [JsonProperty("Dependencies")]
        public Dependency[] Dependencies { get; set; }
    }

    public class Dependency
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Author")]
        public string Author { get; set; }

        [JsonProperty("License")]
        public string License { get; set; }
    }
}
