using MahApps.Metro.Controls;
using ModsUpdateUI.Configurations;
using ModsUpdateUI.Views;

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
        }

        private void CheckUpdateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void WikiButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void AboutButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AboutView view = new AboutView();
            view.Show();
        }

        private void ModsDownloadConfigButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ModsDownloadConfigView view = new ModsDownloadConfigView();
            view.Show();
        }

        private void ModsUpdateConfigButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ModsUpdateConfigView view = new ModsUpdateConfigView();
            view.Show();
        }

        private void StoreFilesConfigButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void DownloadModsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!ConfigurationManager.DownloadModsConfiguration.IsOK)
                return;
            ModsDownloadView view = new ModsDownloadView();
            view.Show();
        }

        private void UpdateModsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!ConfigurationManager.UpdateModsConfiguration.IsOK)
                return;
            ModsUpdateView view = new ModsUpdateView();
            view.Show();
        }
    }
}
