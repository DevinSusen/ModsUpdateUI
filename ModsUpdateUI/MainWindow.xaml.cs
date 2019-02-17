using MahApps.Metro.Controls;
using ModsUpdateUI.Configurations;
using ModsUpdateUI.Services;
using ModsUpdateUI.Views;
using System.Diagnostics;
using System.Windows;

namespace ModsUpdateUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            CheckUpdateAsync();
        }

        private async void CheckUpdateAsync()
        {
            ApplicationService service = new ApplicationService();
            if (await service.CheckUpdateAsync())
            {
                var result = MessageBox.Show("有更新，是否下载并更新？", "检查更新", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Process.Start(Constants.UpdateApplication);
                    Process.GetCurrentProcess().Kill();
                }
            }
            else
            {
                MessageBox.Show("当前已是最新版本", "检查更新");
            }
        }

        private void CheckUpdateButton_Click(object sender, RoutedEventArgs e) => CheckUpdateAsync();

        private void WikiButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AboutView view = new AboutView();
            view.Show();
        }

        private void ModsDownloadConfigButton_Click(object sender, RoutedEventArgs e)
        {
            ModsDownloadConfigView view = new ModsDownloadConfigView();
            view.Show();
        }

        private void ModsUpdateConfigButton_Click(object sender, RoutedEventArgs e)
        {
            ModsUpdateConfigView view = new ModsUpdateConfigView();
            view.Show();
        }

        private void StoreFilesConfigButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DownloadModsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ConfigurationManager.DownloadModsConfiguration.IsOK)
                return;
            ModsDownloadView view = new ModsDownloadView();
            view.Show();
        }

        private void UpdateModsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ConfigurationManager.UpdateModsConfiguration.IsOK)
                return;
            ModsUpdateView view = new ModsUpdateView();
            view.Show();
        }
    }
}
