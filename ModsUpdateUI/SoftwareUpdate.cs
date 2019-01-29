using Octokit;
using System.Threading.Tasks;

namespace ModsUpdateUI
{
    class SoftwareUpdate
    {
        public string OwnerName { get; set; }
        public string Repository { get; set; }

        public async Task<bool> CanUpdate()
        {
            var client = new GitHubClient(new ProductHeaderValue("ModsUpdate"));
            var result = await client.Repository.Release.GetLatest(OwnerName, Repository);
            double newVersion = double.Parse(result.TagName.Substring(1));
            double oldVersion = ConfigLoader.LoadVersion();
            return newVersion > oldVersion;
        }
    }
}
