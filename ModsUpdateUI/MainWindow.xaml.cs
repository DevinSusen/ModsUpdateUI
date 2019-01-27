using ModsUpdateUI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            LoadConfig();
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
