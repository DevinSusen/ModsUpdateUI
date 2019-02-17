using Octokit;
using System.Threading.Tasks;

namespace ModsUpdateUI.Services
{
    internal class ApplicationService
    {
        public async Task<bool> CheckUpdateAsync()
        {
            GithubService service = new GithubService("DevinSusen", "ModsUpdateUI");
            var result = await service.GetLastestReleaseAsync();
            double newVersion = double.Parse(result.TagName.Substring(1));
            double oldVersion = double.Parse(Configurations.ConfigurationManager.SoftwareInformation.Version);
            return newVersion > oldVersion;
        }
    }
}
