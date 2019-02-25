using Octokit;
using ShellProgressBar;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AppUpdate
{
    class Program
    {
        private static ProgressBar _bar;

        static async Task Main(string[] args)
        {
            var (uri, size) = await GetAppUriAsync();

            var options = new ProgressBarOptions
            {
                ProgressBarOnBottom = true,
                CollapseWhenFinished = true
            };
            using (_bar = new ProgressBar(size, "Downloading...", options))
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    client.DownloadFileAsync(uri, Path.GetTempPath() + Path.GetFileName(uri.AbsolutePath));
                    while (client.IsBusy) ;

                    Console.WriteLine("下载完成");
                    Process.Start("cmd.exe", "/k ..\\update.bat ModsUpdateUI.7z");
                    Process.GetCurrentProcess().Kill();
                }
            }
        }

        private static async Task<(Uri, int)> GetAppUriAsync()
        {
            GitHubClient _client = new GitHubClient(new ProductHeaderValue("Update"));
            Release result = null;
            try
            {
                result = await _client.Repository.Release.GetLatest("DevinSusen", "ModsUpdateUI");
            }
            catch (Exception)
            {
                Console.WriteLine("网络错误");
                Environment.Exit(1);
            }
            
            Uri u = new Uri(result.Assets[0].BrowserDownloadUrl);
            int size = result.Assets[0].Size;
            return (u, size);
        }

        private static void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            for (int i = 0; i < e.BytesReceived; i++)
                _bar.Tick();
        }
    }
}
