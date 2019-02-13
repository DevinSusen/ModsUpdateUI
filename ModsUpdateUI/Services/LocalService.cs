using ModsUpdateUI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace ModsUpdateUI.Services
{
    public class LocalService
    {
        /// <summary>
        /// 本机存放目录
        /// </summary>
        public string LocalPath { get; }

        /// <summary>
        /// 元信息文件
        /// </summary>
        public string InfoFileName { get; }

        /// <summary>
        /// 构造LocalService
        /// </summary>
        /// <param name="path">本机存放目录</param>
        /// <param name="infoFileName">元信息文件</param>
        public LocalService(string path, string infoFileName)
        {
            LocalPath = path;
            InfoFileName = infoFileName;
        }

        /// <summary>
        /// 获得本机已下载的Mod
        /// </summary>
        /// <param name="path">Mods文件夹路径</param>
        /// <param name="infoFileName">Mod元信息文件</param>
        /// <returns>所有Mod信息的集合</returns>
        public List<LocalModInfo> FromDirectory()
        {
            List<LocalModInfo> infos = new List<LocalModInfo>();

            foreach (var i in Directory.EnumerateDirectories(LocalPath))
            {
                DirectoryInfo info = new DirectoryInfo(i);
                string infoFilePath = info.FullName + @"\" + InfoFileName;
                if (!File.Exists(infoFilePath))
                    continue;
                string content = File.ReadAllText(infoFilePath);
                infos.Add(JsonConvert.DeserializeObject<LocalModInfo>(content));
            }
            return infos;
        }
    }
}
