using ModsUpdateUI.Configurations;

namespace ModsUpdateUI.Services
{
    public static class ServiceManager
    {
        private static LocalService _localService = null;

        public static LocalService LocalService
        {
            get
            {
                if (_localService == null)
                    _localService = new LocalService(ConfigurationManager.UpdateModsConfiguration.ModsDirectory, ConfigurationManager.UpdateModsConfiguration.InfoFile);
                return _localService;
            }
        }

        private static GithubService _githubService = null;

        public static GithubService GithubService
        {
            get
            {
                if (_githubService == null)
                    _githubService = new GithubService(ConfigurationManager.DownloadModsConfiguration.Owner, ConfigurationManager.DownloadModsConfiguration.Repository);
                return _githubService;
            }
        }
    }
}
