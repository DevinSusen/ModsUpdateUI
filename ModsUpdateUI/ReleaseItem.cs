using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsUpdateUI
{
    public class ReleaseItem
    {
        public ReleaseItem(string name, int size, string download, int id, DateTime update, DateTime create, string fileType)
        {
            Name = name;
            Size = size;
            DownloadUrl = download;
            ID = id;
            UpdateTime = update;
            CreateTime = create;
            FileType = fileType;
        }

        /// <summary>
        /// 项目名字
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 项目大小，单位Byte
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// 下载的Url
        /// </summary>
        public string DownloadUrl { get; private set; }

        /// <summary>
        /// 该版本项目的ID
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; private set; }

        /// <summary>
        /// 项目文件类型
        /// </summary>
        public string FileType { get; private set; }
    }
}
