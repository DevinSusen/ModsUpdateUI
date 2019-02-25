using ModsUpdateUI.Utils;
using Newtonsoft.Json;
using System.IO;

namespace ModsUpdateUI.Configurations
{
    public class UpdateModsConfig
    {
        /// <summary>
        /// Mod的存放目录
        /// </summary>
        [JsonProperty("ModsDirectory")]
        public string ModsDirectory { get; set; } = Utilities.GetTheScrollOfTaiwuModsFolder();

        /// <summary>
        /// 所有者
        /// </summary>
        [JsonProperty("Owner")]
        public string Owner { get; set; } = "phorcys";

        /// <summary>
        /// 仓库名
        /// </summary>
        [JsonProperty("Repository")]
        public string Repository { get; set; } = "Taiwu_mods";

        /// <summary>
        /// 要查看的文件类型
        /// </summary>
        [JsonProperty("FileType")]
        public string FileType { get; set; } = "application/zip";

        /// <summary>
        /// 存储信息文件
        /// </summary>
        [JsonProperty("InfoFile")]
        public string InfoFile { get; set; } = "Info.json";

        /// <summary>
        /// 该配置是否已经合法
        /// </summary>
        /// <returns>如果true，则配置生效</returns>
        [JsonIgnore]
        public bool IsOK { get => Directory.Exists(ModsDirectory); }

        /// <summary>
        /// 是否可以可以更新
        /// </summary>
        /// <returns>如果true，则可以开始更新</returns>
        [JsonIgnore]
        public bool CanUpdate { get => !string.IsNullOrWhiteSpace(Owner) && !string.IsNullOrWhiteSpace(Repository); }
    }
}
