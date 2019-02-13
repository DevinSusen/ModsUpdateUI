using ModsUpdateUI.Models;
using System.Collections.Generic;

namespace ModsUpdateUI.Interfaces
{
    /// <summary>
    /// 更新Mod的接口
    /// </summary>
    public interface IModUpdate
    {
        /// <summary>
        /// 根据当前的Mod获得对应的最新版本Mod
        /// </summary>
        /// <param name="modInfo">本机上的Mod</param>
        /// <returns>服务器上的Mod</returns>
        RemoteModInfo GetLatestMod(LocalModInfo modInfo);

        /// <summary>
        /// 更新Mod
        /// </summary>
        /// <param name="modInfo">服务器上的Mod</param>
        /// <param name="path">Mod存储文件夹</param>
        void UpdateMod(RemoteModInfo modInfo, string path);

        /// <summary>
        /// 更新所有的Mod
        /// </summary>
        /// <param name="modInfos">服务器上的所有可更新Mod</param>
        /// <param name="path">Mod存储文件夹</param>
        void UpdateAllMods(List<RemoteModInfo> modInfos, string path);
    }
}
