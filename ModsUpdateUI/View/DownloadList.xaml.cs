using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace ModsUpdateUI.View
{
    /// <summary>
    /// DownloadList.xaml 的交互逻辑
    /// </summary>
    public partial class DownloadList : Window
    {
        public DownloadList()
        {
            InitializeComponent();

            DataContext = this;
            OwnerTextBox.Text = Config.DownloadConfig.Owner;
            ReposTextBox.Text = Config.DownloadConfig.Repository;
        }

        public ObservableCollection<ReleaseItem> ReleaseItems { get; set; } = new ObservableCollection<ReleaseItem>();

        private void DefaultConfig_Click(object sender, RoutedEventArgs e)
        {
            DownloadConfigUI newWindow = new DownloadConfigUI();
            newWindow.Show();
        }

        private void LoadData(string owner, string repos, string fileType)
        {
            GitHubConnect connect = new GitHubConnect(owner, repos);
            ModsRelease mods = Task.Run(connect.ParseReleaseInfoAsync).GetAwaiter().GetResult();
            if (fileType == "all")
            {
                foreach (var i in mods.Assets)
                    ReleaseItems.Add(i);
            }
            else
            {
                foreach (var i in mods.GetItemsByFileType(fileType))
                    ReleaseItems.Add(i);
            }
        }

        private string GetOwnerName()
        {
            string ownerName = OwnerTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(ownerName))
                return Config.DownloadConfig.Owner;
            return ownerName;
        }

        private string GetRepos()
        {
            string repos = ReposTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(repos))
                return Config.DownloadConfig.Repository;
            return repos;
        }

        private void LoadDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (ModItemsListView.HasItems)
                ReleaseItems.Clear();
            LoadData(GetOwnerName(), GetRepos(), Config.DownloadConfig.FileType);
        }

        private async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            if (ModItemsListView.SelectedItems.Count == 0)
                MessageBox.Show("请选择要下载的项目（一项或多项）");
            if (!Directory.Exists(Config.DownloadConfig.DownloadDir))
                MessageBox.Show("请前往默认设置，设置下载目录");

            ConcurrentBag<ReleaseItem> items = new ConcurrentBag<ReleaseItem>();
            foreach (var i in ModItemsListView.SelectedItems)
                items.Add(i as ReleaseItem);

            if (Config.DownloadConfig.IsDecompress)
            {
                await Task.Run(() => Parallel.ForEach(items, DownloadAndDecompressFile));
                MessageBox.Show("下载完成");
            }
            else
            {
                await Task.Run(() => Parallel.ForEach(items, DownloadFile));
                MessageBox.Show("下载完成");
            }
        }

        private void DownloadAndDecompressFile(ReleaseItem item)
        {
            DownloadFile(item);
            string path = Config.DownloadConfig.DownloadDir + @"\" + item.Name;
            System.IO.Compression.ZipFile.ExtractToDirectory(path, Config.DownloadConfig.DownloadDir);
            File.Delete(path);
        }

        private void DownloadFile(ReleaseItem item)
        {
            using (var client = new System.Net.WebClient())
            {
                string path = Config.DownloadConfig.DownloadDir + @"\" + item.Name;
                client.DownloadFile(new System.Uri(item.DownloadUrl), path);
            }
        }
    }
}
