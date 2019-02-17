using Octokit;
using ShellProgressBar;
using System;
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
                ProgressBarOnBottom = true
            };
            using (_bar = new ProgressBar(size, "Downloading...", options))
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    client.DownloadFileCompleted += Client_DownloadFileCompleted;

                    await Task.Run(() => Download(client, uri));
                }
            }
        }

        private static void Download(WebClient client, Uri uri)
        {
            client.DownloadFileAsync(uri, Path.GetFileName(uri.AbsolutePath));
            while (client.IsBusy) ;
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

        private static void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Console.WriteLine("下载完成");
        }

        private static void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            for (int i = 0; i < e.BytesReceived; i++)
                _bar.Tick();
        }
    }
}
