namespace ModsUpdateUI
{
    class CheckUpdateConfig
    {
        public CheckUpdateConfig() { }

        public CheckUpdateConfig(string m, string res, string own, string ft, string infoF)
        {
            ModsDir = m;
            Owner = own;
            Repository = res;
            FileType = ft;
            InfoFile = infoF;
        }

        /// <summary>
        /// Mod的存放目录
        /// </summary>
        public string ModsDir { get; set; }

        /// <summary>
        /// 所有者
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// 仓库名
        /// </summary>
        public string Repository { get; set; }

        /// <summary>
        /// 要查看的文件类型
        /// </summary>
        public string FileType { get; set; } = "application/zip";

        /// <summary>
        /// 存储信息文件
        /// </summary>
        public string InfoFile { get; set; } = "Info.json";
    }
}
