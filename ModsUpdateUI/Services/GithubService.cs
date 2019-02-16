using ModsUpdateUI.Models;
using Octokit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModsUpdateUI.Services
{
    public class GithubService
    {
        private GitHubClient _client = new GitHubClient(new ProductHeaderValue("user"));

        public string Owner { get; }

        public string Repos { get; }

        public GithubService(string owner, string repos)
        {
            Owner = owner;
            Repos = repos;
        }

        public async Task<List<RemoteModInfo>> GetLastestModsAsync()
        {
            Release result;
            List<RemoteModInfo> mods = new List<RemoteModInfo>();
            try
            {
                result = await _client.Repository.Release.GetLatest(Owner, Repos);
            }
            catch (System.Exception)
            {
                return mods;
            }
            
            foreach (var i in result.Assets)
            {
                RemoteModInfo info = new RemoteModInfo
                {
                    BrowserDownloadUrl = i.BrowserDownloadUrl,
                    ContentType = i.ContentType,
                    Id = i.Id,
                    Name = i.Name,
                    Size = i.Size,
                    Updated = i.UpdatedAt.DateTime
                };
                mods.Add(info);
            }
            return mods;
        }
    }
}
