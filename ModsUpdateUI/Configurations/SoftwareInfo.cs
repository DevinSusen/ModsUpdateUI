using Newtonsoft.Json;
using System;

namespace ModsUpdateUI.Configurations
{
    public class SoftwareInfo
    {
        [JsonProperty("Author")]
        public string Author { get; set; } = "Devin Tung";

        [JsonProperty("HomePage")]
        public Uri HomePage { get; set; } = new Uri(@"https://github.com/DevinSusen/ModsUpdateUI");

        [JsonProperty("Version")]
        public string Version { get; set; } = "1.2";

        [JsonProperty("ReleaseTime")]
        public DateTime ReleaseTime { get; set; } = DateTime.Now;

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
