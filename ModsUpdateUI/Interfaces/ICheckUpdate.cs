using ModsUpdateUI.Models;
using System.Collections.Generic;

namespace ModsUpdateUI.Interfaces
{
    /// <summary>
    /// 检查更新的接口
    /// </summary>
    public interface ICheckUpdate
    {
        /// <summary>
        /// 检查某个Mod是否可更新
        /// </summary>
        /// <param name="modInfo">本机上的某个Mod</param>
        /// <returns>如果true，代表可更新，否则不可更新</returns>
        bool CanUpdate(LocalModInfo modInfo);

        /// <summary>
        /// 获得所有可更新Mod
        /// </summary>
        /// <returns>所有可更新Mod</returns>
        List<LocalModInfo> AllUpdatableMods();
    }
}
