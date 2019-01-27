using Octokit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModsUpdateUI
{
    class GitHubConnect
    {
        public GitHubConnect(string owerName, string repositoryName)
        {
            OwnerName = owerName;
            RepositoryName = repositoryName;
        }

        /// <summary>
        /// 拥有者
        /// </summary>
        public string OwnerName { get; private set; }

        /// <summary>
        /// 代码仓库名
        /// </summary>
        public string RepositoryName { get; private set; }

        /// <summary>
        /// 解析返回的JSON文本，并设置各项
        /// </summary>
        /// <returns>该次Release的项目</returns>
        public async Task<ModsRelease> ParseReleaseInfoAsync()
        {
            if (string.IsNullOrEmpty(OwnerName) || string.IsNullOrEmpty(RepositoryName))
                throw new ArgumentException("请设置仓库及所有者");
            var client = new GitHubClient(new ProductHeaderValue("taiwu"));
            var result = await client.Repository.Release.GetLatest(OwnerName, RepositoryName);
            DateTime publishTime = new DateTime();
            if (result.PublishedAt.HasValue)
                publishTime = result.PublishedAt.Value.DateTime;

            List<ReleaseItem> infos = new List<ReleaseItem>();
            foreach (var i in result.Assets)
            {
                ReleaseItem item = new ReleaseItem(i.Name, i.Size, i.BrowserDownloadUrl, i.Id, i.UpdatedAt.DateTime, i.CreatedAt.DateTime, i.ContentType);
                infos.Add(item);
            }

            return new ModsRelease(result.HtmlUrl, result.Id, publishTime, result.CreatedAt.DateTime, result.Author.Login, infos);
        }
    }
}
