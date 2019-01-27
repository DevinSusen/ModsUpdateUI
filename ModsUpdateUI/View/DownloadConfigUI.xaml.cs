using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace ModsUpdateUI.View
{
    /// <summary>
    /// DownloadConfig.xaml 的交互逻辑
    /// </summary>
    public partial class DownloadConfigUI : Window
    {
        public DownloadConfigUI()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            DownloadConfig config = Config.DownloadConfig;
            OwnerText.Text = config.Owner;
            ReposText.Text = config.Repository;
            FileTypeSelect.Text = config.FileType;
            FolderSelect.Text = config.DownloadDir;
            DecompressChecked.IsChecked = config.IsDecompress;
            NotDecompress.IsChecked = !DecompressChecked.IsChecked;
        }

        private void FolderSelect_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                SelectedPath = AppDomain.CurrentDomain.BaseDirectory
            };
            DialogResult result = folderDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
                FolderSelect.Text = folderDialog.SelectedPath;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string ownerName = OwnerText.Text.Trim();
            string repos = ReposText.Text.Trim();
            string fileType = FileTypeSelect.Text;
            string path = FolderSelect.Text;
            bool autoDecompress = DecompressChecked.IsChecked ?? false;
            DownloadConfig config = new DownloadConfig(ownerName, repos, fileType, path, autoDecompress);
            ConfigLoader.SaveDownloadConfig(config);
            Config.DownloadConfig = config;
            System.Windows.MessageBox.Show("保存成功");
        }
    }
}
