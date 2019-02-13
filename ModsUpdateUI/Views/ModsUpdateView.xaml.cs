using MahApps.Metro.Controls;
using ModsUpdateUI.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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

            DataContext = _model;
        }

        private ModsUpdateViewModel _model = new ModsUpdateViewModel();

        private void UpdatableModsButton_Click(object sender, RoutedEventArgs e) => _model.ShowUpdatableMods();

        private void AllModsButton_Click(object sender, RoutedEventArgs e) => _model.ShowAllMods();

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }

}
