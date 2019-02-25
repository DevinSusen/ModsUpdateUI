using Newtonsoft.Json;
using System.IO;

namespace ModsUpdateUI.Configurations
{
    public static class ConfigurationManager
    {
        #region Download Configuration
        private static DownloadModsConfig _downloadModsConfig = null;

        public static DownloadModsConfig DownloadModsConfiguration
        {
            get
            {
                if (_downloadModsConfig == null)
                    _downloadModsConfig = LoadConfig<DownloadModsConfig>(Constants.DownloadModsConfigPath);
                return _downloadModsConfig;
            }
        }
        #endregion

        #region Update Mods Configuration
        private static UpdateModsConfig _updateModsConfig = null;

        public static UpdateModsConfig UpdateModsConfiguration
        {
            get
            {
                if (_updateModsConfig == null)
                    _updateModsConfig = LoadConfig<UpdateModsConfig>(Constants.UpdateModsConfigPath);
                return _updateModsConfig;
            }
        }
        #endregion

        #region Load Configuration

        private static T LoadConfig<T>(string fileName) where T : new()
        {
            if (!File.Exists(fileName))
                return InitConfiguration<T>(fileName);
            string s = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<T>(s);
        }

        private static T InitConfiguration<T>(string fileName) where T : new()
        {
            T t = new T();
            SaveConfiguration(t, fileName);
            return t;
        }
        #endregion

        #region Save Configuration

        public static void SaveConfiguration<T>(T t, string fileName) where T : new()
        {
            string s = JsonConvert.SerializeObject(t, Formatting.Indented);
            File.WriteAllText(fileName, s);
        }

        #endregion

        #region Software Mata Data

        private static SoftwareInfo _sinfo = null;

        public static SoftwareInfo SoftwareInformation
        {
            get
            {
                if (_sinfo == null)
                    _sinfo = LoadConfig<SoftwareInfo>(Constants.SoftwareMataDataPath);
                return _sinfo;
            }
        }

        #endregion
    }
}
