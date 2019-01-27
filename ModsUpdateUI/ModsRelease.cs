using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsUpdateUI
{
    class ModsRelease
    {
        public ModsRelease(string htmlUrl, int id, DateTime publishTime, DateTime createTime, string auth, List<ReleaseItem> items)
        {
            HtmlUrl = htmlUrl;
            ID = id;
            PublishTime = publishTime;
            CreateTime = createTime;
            AuthorName = auth;
            Assets = items;
        }

        /// <summary>
        /// 浏览器显示的HTML url
        /// </summary>
        public string HtmlUrl { get; private set; }

        /// <summary>
        /// 该版本Release的ID，用于和上一版本比较，更新大版本
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Release发布的时间
        /// </summary>
        public DateTime PublishTime { get; private set; }

        /// <summary>
        /// 该版本某次更新的时间，可用于更新小版本
        /// </summary>
        public DateTime CreateTime { get; private set; }

        /// <summary>
        /// Release的作者
        /// </summary>
        public string AuthorName { get; private set; }

        /// <summary>
        /// 更新发布的所有内容
        /// </summary>
        public List<ReleaseItem> Assets { get; private set; }

        /// <summary>
        /// 通过文件类型筛选
        /// </summary>
        /// <param name="fileType">文件类型</param>
        /// <returns>符合文件类型的所有项目的集合</returns>
        public IEnumerable<ReleaseItem> GetItemsByFileType(string fileType)
        {
            var result = from r in Assets where r.FileType == fileType select r;
            return result;
        }
    }
}
