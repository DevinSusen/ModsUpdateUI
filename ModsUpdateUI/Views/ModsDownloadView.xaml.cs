using MahApps.Metro.Controls;
using ModsUpdateUI.ViewModels;
using System.Windows.Controls;

namespace ModsUpdateUI.Views
{
    /// <summary>
    /// ModsDownloadView.xaml 的交互逻辑
    /// </summary>
    public partial class ModsDownloadView : MetroWindow
    {
        public ModsDownloadView()
        {
            InitializeComponent();

            DataContext = _model;
            DataListView.ItemContainerGenerator.ItemsChanged += ItemContainerGenerator_ItemsChanged;
        }

        private void ItemContainerGenerator_ItemsChanged(object sender, System.Windows.Controls.Primitives.ItemsChangedEventArgs e) => DataLoadedProgressBar.IsIndeterminate = false;

        private ModsDownloadViewModel _model = new ModsDownloadViewModel();

        private void AllModsButton_Click(object sender, System.Windows.RoutedEventArgs e) => _model.ShowAll();

        private void UndownloadItemButton_Click(object sender, System.Windows.RoutedEventArgs e) => _model.ShowUndownloaded();

        private void DownloadedItemButton_Click(object sender, System.Windows.RoutedEventArgs e) => _model.ShowDownloaded();
    }
}
