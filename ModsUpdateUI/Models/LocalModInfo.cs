using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ModsUpdateUI.Models
{
    public class LocalModInfo
    {
        /// <summary>
        /// 标识
        /// </summary>
        [JsonProperty("Id")]
        public string Id { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [JsonProperty("Author")]
        public string Author { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        [JsonProperty("Version")]
        public string Version { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        [JsonProperty("AssemblyName")]
        public string AssemblyName { get; set; }

        /// <summary>
        /// 加载方法
        /// </summary>
        [JsonProperty("EntryMethod")]
        public string EntryMethod { get; set; }

        /// <summary>
        /// 该Mod的前置需求
        /// </summary>
        [JsonProperty("Requirements")]
        public string[] Requirements { get; set; }

        /// <summary>
        /// 主页
        /// </summary>
        [JsonProperty("HomePage")]
        public Uri HomePage { get; set; }

        /// <summary>
        /// Mod地址
        /// </summary>
        [JsonProperty("Repository")]
        public string Repository { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [JsonProperty("Description")]
        public string Description { get; set; }

        /// <summary>
        /// 管理器版本
        /// </summary>
        [JsonProperty("ManagerVersion")]
        public string ManagerVersion { get; set; }
    }
}
