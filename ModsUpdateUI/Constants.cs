namespace ModsUpdateUI
{
    public static class Constants
    {
        #region Steam Key
        public const string SteamRegistryKey64 = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam";
        public const string SteamRegistryKey32 = @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam";
        #endregion

        #region TaiWu Mods
        public const string TaiWuModsFolder = @"\steamapps\common\The Scroll Of Taiwu\Mods";
        #endregion

        #region Configuration
        public const string DownloadModsConfigPath = @"DownloadModsConfig.json";
        public const string UpdateModsConfigPath = @"UpdateModsConfig.json";

        public const string SoftwareMataDataPath = @"MataData.json";
        #endregion

        #region File Type

        public const string ZipType = @"application/zip";
        public const string JsonType = @"application/json";

        #endregion

        #region CheckUpdate

        public const string UpdateApplication = @"AppUpdate.exe";

        #endregion
    }
}
