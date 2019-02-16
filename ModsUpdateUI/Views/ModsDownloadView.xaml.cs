using MahApps.Metro.Controls;
using ModsUpdateUI.ViewModels;
using System.Collections.Generic;

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

            _model = new ModsDownloadViewModel(AlertSnackbar.MessageQueue);
            DataContext = _model;
            DataListView.ItemContainerGenerator.ItemsChanged += ItemContainerGenerator_ItemsChanged;
        }

        private void ItemContainerGenerator_ItemsChanged(object sender, System.Windows.Controls.Primitives.ItemsChangedEventArgs e) => DataLoadedProgressBar.IsIndeterminate = false;

        private ModsDownloadViewModel _model;

        private void AllModsButton_Click(object sender, System.Windows.RoutedEventArgs e) => _model.ShowAll();

        private void UndownloadItemButton_Click(object sender, System.Windows.RoutedEventArgs e) => _model.ShowUndownloaded();

        private void DownloadedItemButton_Click(object sender, System.Windows.RoutedEventArgs e) => _model.ShowDownloaded();

        private void DownloadButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<DownloadModItem> items = new List<DownloadModItem>();
            foreach(var i in DataListView.SelectedItems)
                items.Add(i as DownloadModItem);
            _model.DownloadItems = items;
            _model.DownloadMod();
        }
    }
}
