using Newtonsoft.Json;

namespace ModsUpdateUI
{
    class ConfigLoader
    {
        #region Download
        public static DownloadConfig LoadDownloadConfig()
        {
            if (!System.IO.File.Exists("download.json"))
                return InitDownloadConfig();
            string s = System.IO.File.ReadAllText("download.json");
            DownloadConfig cofig = JsonConvert.DeserializeObject<DownloadConfig>(s);
            return cofig;
        }

        private static DownloadConfig InitDownloadConfig()
        {
            DownloadConfig config = new DownloadConfig();
            SaveDownloadConfig(config);
            return config;
        }

        public static void SaveDownloadConfig(DownloadConfig config)
        {
            string s = JsonConvert.SerializeObject(config, Formatting.Indented);
            System.IO.File.WriteAllText("download.json", s);
        }
        #endregion

        #region Check Update
        public static CheckUpdateConfig LoadCheckUpdateConfig()
        {
            if (!System.IO.File.Exists("checkUpdate.json"))
                return InitCheckUpdateConfig();
            string s = System.IO.File.ReadAllText("checkUpdate.json");
            return JsonConvert.DeserializeObject<CheckUpdateConfig>(s);
        }

        private static CheckUpdateConfig InitCheckUpdateConfig()
        {
            CheckUpdateConfig config = new CheckUpdateConfig();
            SaveCheckUpdateConfig(config);
            return config;
        }

        public static void SaveCheckUpdateConfig(CheckUpdateConfig config)
        {
            string s = JsonConvert.SerializeObject(config, Formatting.Indented);
            System.IO.File.WriteAllText("checkUpdate.json", s);
        }
        #endregion
    }
}
