using MahApps.Metro.Controls;
using ModsUpdateUI.ViewModels;
using System.Collections.Generic;
using System.Windows;

namespace ModsUpdateUI.Views
{
    /// <summary>
    /// ModsUpdateView.xaml 的交互逻辑
    /// </summary>
    public partial class ModsUpdateView : MetroWindow
    {
        public ModsUpdateView()
        {
            InitializeComponent();

            _model = new ModsUpdateViewModel(AlertSnackbar.MessageQueue);
            DataContext = _model;
            DataListView.ItemContainerGenerator.ItemsChanged += ItemContainerGenerator_ItemsChanged;
        }

        private void ItemContainerGenerator_ItemsChanged(object sender, System.Windows.Controls.Primitives.ItemsChangedEventArgs e) => _model.CheckUpdateAsync();

        private ModsUpdateViewModel _model;

        private void UpdatableModsButton_Click(object sender, RoutedEventArgs e) => _model.ShowUpdatableMods();

        private void AllModsButton_Click(object sender, RoutedEventArgs e) => _model.ShowAllMods();

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            List<UpdateModItem> items = new List<UpdateModItem>();
            foreach(var it in DataListView.SelectedItems)
            {
                UpdateModItem item = it as UpdateModItem;
                if (!item.CanUpdate)
                    continue;
                items.Add(item);
            }
            _model.UpdateItems = items;
            _model.UpdateMod();
        }
    }

}
