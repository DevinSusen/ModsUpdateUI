using System;
using System.IO;

namespace ModsUpdateUI.Models
{
    public class RemoteModInfo
    {
        /// <summary>
        /// Mod的唯一标识符
        /// </summary>
        public int Id { get; set; }  // "id": 10918193

        /// <summary>
        /// Mod名称
        /// </summary>
        public string Name { get; set; }  // "name": "AllWin-1.0.1.zip"

        /// <summary>
        /// 文件类型
        /// </summary>
        public string ContentType { get; set; } // "content_type": "application/zip",

        /// <summary>
        /// 版本
        /// </summary>
        public string Version  // "name": "AllWin-1.0.1.zip" -> "1.0.1"
        {
            get
            {
                string fileName = Path.GetFileNameWithoutExtension(Name);
                int idx = fileName.IndexOf('-');
                if (idx == -1)
                    return "?";
                return fileName.Substring(idx+1);
            }
        } 

        /// <summary>
        /// 大小 以Byte为单位
        /// </summary>
        public int Size { get; set; } // "size": 2723,

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime Updated { get; set; }// "updated_at": "2019-02-04T08:56:54Z",

        /// <summary>
        /// 下载地址
        /// </summary>
        public string BrowserDownloadUrl { get; set; } // "browser_download_url": "https://github.com/phorcys/Taiwu_mods/releases/download/release190204.1/AllWin-1.0.1.zip"
    }
}
