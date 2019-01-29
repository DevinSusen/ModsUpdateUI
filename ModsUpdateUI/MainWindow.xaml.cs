using ModsUpdateUI.View;
using System.Threading.Tasks;
using System.Windows;

namespace ModsUpdateUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MWindow.Title = "Mod更新辅助工具 \t----\t版本：" + ConfigLoader.LoadVersion();
            LoadConfig();
            CheckUpdate();
        }

        private async void CheckUpdate()
        {
            SoftwareUpdate su = new SoftwareUpdate
            {
                OwnerName = "DevinSusen",
                Repository = "ModsUpdateUI"
            };
            bool canUpdate = await Task.Run(su.CanUpdate);
            if (canUpdate)
            {
                var res = MessageBox.Show("软件可更新，是否打开浏览器去下载？", "软件更新", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                    System.Diagnostics.Process.Start("https://github.com/DevinSusen/ModsUpdateUI/releases");
            }
        }

        private void ToDownload_Click(object sender, RoutedEventArgs e)
        {
            DownloadList newWindow = new DownloadList();
            newWindow.Show();
            Close();
        }

        private void ToCheckUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update newWindow = new Update();
            newWindow.Show();
            Close();
        }

        private void LoadConfig()
        {
            Config.UpdateConfig = ConfigLoader.LoadCheckUpdateConfig();
            Config.DownloadConfig = ConfigLoader.LoadDownloadConfig();
        }
    }
}
