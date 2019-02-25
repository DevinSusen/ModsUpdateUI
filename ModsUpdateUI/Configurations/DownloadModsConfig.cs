using Newtonsoft.Json;
using System;
using System.IO;

namespace ModsUpdateUI.Configurations
{
    public class DownloadModsConfig
    {
        /// <summary>
        /// 下载目录
        /// </summary>
        [JsonProperty("DownloadDirectory")]
        public string DownloadDirectory { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        /// <summary>
        /// 是否自动解压
        /// </summary>
        [JsonProperty("IsAutoDecompress")]
        public bool IsAutoDecompress { get; set; } = false;

        /// <summary>
        /// 解压后是否删除原文件
        /// </summary>
        [JsonProperty("IsDeleteFileWhenDecompress")]
        public bool IsDeleteFileWhenDecompress { get; set; } = true;

        /// <summary>
        /// Github账户名
        /// </summary>
        [JsonProperty("Owner")]
        public string Owner { get; set; } = "phorcys";

        /// <summary>
        /// 代码库
        /// </summary>
        [JsonProperty("Repository")]
        public string Repository { get; set; } = "Taiwu_mods";

        /// <summary>
        /// 该配置是否已经合法
        /// </summary>
        /// <returns>如果true，则配置生效</returns>
        [JsonIgnore]
        public bool IsOK { get => !string.IsNullOrWhiteSpace(Owner) && !string.IsNullOrWhiteSpace(Repository); }

        /// <summary>
        /// 是否可以下载
        /// </summary>
        /// <returns>如果true，则可以开始下载</returns>
        [JsonIgnore]
        public bool CanDownload { get => Directory.Exists(DownloadDirectory); }
    }
}
