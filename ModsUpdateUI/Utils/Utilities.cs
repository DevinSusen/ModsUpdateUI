using Microsoft.Win32;

namespace ModsUpdateUI.Utils
{
    public static class Utilities
    {
        /// <summary>
        /// Get Steam's installation path
        /// </summary>
        /// <returns>null if can't find it. The path if it exists.</returns>
        public static string GetSteamFolder()
        {
            object value = Registry.GetValue(Constants.SteamRegistryKey64, @"InstallPath", null);
            if(value == null)
                value = Registry.GetValue(Constants.SteamRegistryKey32, @"InstallPath", null);
            if (value == null)
                return null;
            return value as string;
        }

        /// <summary>
        /// Get the mods folder of The Scroll Of Taiwu.
        /// </summary>
        /// <returns>empty string if can't find it</returns>
        public static string GetTheScrollOfTaiwuModsFolder()
        {
            string steamPath = GetSteamFolder();
            if (steamPath == null)
                return "";
            return steamPath + Constants.TaiWuModsFolder;
        }
    }
}
