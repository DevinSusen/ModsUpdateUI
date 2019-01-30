using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace ModsUpdateUI.View
{
    /// <summary>
    /// Update.xaml 的交互逻辑
    /// </summary>
    public partial class Update : Window
    {
        public Update()
        {
            InitializeComponent();

            DataContext = this;

            OwnerTextBox.Text = Config.UpdateConfig.Owner;
            ReposTextBox.Text = Config.UpdateConfig.Repository;
            ModsTextBox.Text = Config.UpdateConfig.ModsDir;
        }

        public ObservableCollection<UpdateItem> Items { get; set; } = new ObservableCollection<UpdateItem>();

        private List<ReleaseItem> _releaseItems = null;

        private void CheckUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string path = ModsTextBox.Text.Trim();
            if (!Directory.Exists(path))
                path = Config.UpdateConfig.ModsDir;
            string repos = GetRepos();
            string owner = GetOwnerName();
            if (string.IsNullOrWhiteSpace(repos) || string.IsNullOrWhiteSpace(owner) || !Directory.Exists(path))
            {
                System.Windows.MessageBox.Show("请检查您的输入");
                return;
            }

            GitHubConnect con = new GitHubConnect(owner, repos);
            _releaseItems = Task.Run(con.ParseReleaseInfoAsync).GetAwaiter().GetResult().GetItemsByFileType(Config.UpdateConfig.FileType).ToList();
            var dic = new Dictionary<string, string>();
            _releaseItems.ForEach(v => { int idx = v.Name.LastIndexOf('-'); int dotIdx = v.Name.LastIndexOf('.'); dic.Add(v.Name.Substring(0, idx), v.Name.Substring(idx + 1, dotIdx-idx-1)); });

            List<ReleaseItem> item_ = new List<ReleaseItem>();

            foreach (var i in Directory.EnumerateDirectories(path))
            {
                DirectoryInfo info = new DirectoryInfo(i);
                UpdateItem item = new UpdateItem
                {
                    ModName = info.Name
                };

                ReleaseItem itm = _releaseItems.Find(it => it.Name.StartsWith(info.Name));
                if(itm != null)
                    item_.Add(itm);

                string s = File.ReadAllText(info.FullName + @"\" + Config.UpdateConfig.InfoFile);
                JObject jo = JObject.Parse(s);
                if (jo.ContainsKey("Author"))
                    item.Author = (string)jo["Author"];
                if (jo.ContainsKey("DisplayName"))
                    item.DisplayName = (string)jo["DisplayName"];
                if (jo.ContainsKey("Id"))
                    item.ID = (string)jo["Id"];
                if (jo.ContainsKey("Version"))
                    item.CurrentVersion = (string)jo["Version"];
                if (dic.ContainsKey(info.Name))
                {
                    item.LatestVersion = dic[info.Name];
                    if (item.LatestVersion != item.CurrentVersion)
                        item.CanUpdated = true;
                }
                
                Items.Add(item);
            }
            _releaseItems = item_;
        }

        private string GetOwnerName()
        {
            string ownerName = OwnerTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(ownerName))
                return Config.UpdateConfig.Owner;
            return ownerName;
        }

        private string GetRepos()
        {
            string repos = ReposTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(repos))
                return Config.UpdateConfig.Repository;
            return repos;
        }

        private void DefaultConfig_Click(object sender, RoutedEventArgs e)
        {
            UpdateConfig configUI = new UpdateConfig();
            configUI.Show();
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ModItemsListView.SelectedItems.Count == 0)
            {
                System.Windows.MessageBox.Show("请选择要更新的项目");
                return;
            }

            ConcurrentBag<UpdateItem> items = new ConcurrentBag<UpdateItem>();
            foreach (var i in ModItemsListView.SelectedItems)
            {
                UpdateItem it = i as UpdateItem;
                if (it.CanUpdated)
                    items.Add(it);
            }

            await Task.Run(() => Parallel.ForEach(items, new ParallelOptions { MaxDegreeOfParallelism = 4 }, UpdateMod));
            System.Windows.MessageBox.Show("更新完成");
        }

        private void DownloadFile(UpdateItem item)
        {
            using (var client = new WebClient())
            {
                string path = Config.UpdateConfig.ModsDir + @"\" + item.ModName + @".zip";
                string downloadUrl = _releaseItems.Find(v => v.Name.StartsWith(item.ModName)).DownloadUrl;
                client.DownloadFile(new Uri(downloadUrl), path);
            }
        }

        private void UpdateMod(UpdateItem item)
        {
            try
            {
                DownloadFile(item);
            }
            catch (WebException e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return;
            }
            Directory.Delete(Config.UpdateConfig.ModsDir + @"\" + item.ModName, true);
            string path = Config.UpdateConfig.ModsDir + @"\" + item.ModName + @".zip";
            System.IO.Compression.ZipFile.ExtractToDirectory(path, Config.UpdateConfig.ModsDir);
            File.Delete(path);
        }

        private void ModsTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                SelectedPath = AppDomain.CurrentDomain.BaseDirectory
            };
            DialogResult result = folderDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
                ModsTextBox.Text = folderDialog.SelectedPath;
        }

        private void ModsTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ModsTextBox.Text == "双击有奇效")
                return;
            ModsTextBox.Text = "";
        }

        private void ModsTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ModsTextBox.Text))
                ModsTextBox.Text = "双击有奇效";
        }
    }
}
