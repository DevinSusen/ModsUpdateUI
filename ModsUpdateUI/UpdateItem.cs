namespace ModsUpdateUI
{
    public class UpdateItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Mod名称
        /// </summary>
        public string ModName { get; set; }
        /// <summary>
        /// 当前版本
        /// </summary>
        public string CurrentVersion { get; set; }
        /// <summary>
        /// 最新版本
        /// </summary>
        public string LatestVersion { get; set; }
        /// <summary>
        /// 是否有更新
        /// </summary>
        public bool CanUpdated { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get;set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
    }
}
