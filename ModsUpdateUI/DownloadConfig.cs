namespace ModsUpdateUI
{
    class DownloadConfig
    {
        public DownloadConfig() { }

        public DownloadConfig(string o, string r, string f, string d, bool i)
        {
            Owner = o;
            Repository = r;
            FileType = f;
            DownloadDir = d;
            IsDecompress = i;
        }

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
        public string FileType { get; set; } = "all";

        /// <summary>
        /// 下载到该文件夹下
        /// </summary>
        public string DownloadDir { get; set; }

        /// <summary>
        /// 是否解压
        /// </summary>
        public bool IsDecompress { get; set; } = false;
    }
}
