using MahApps.Metro.Controls;
using System;
using ModsUpdateUI.Configurations;
using FolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;
using System.IO;

namespace ModsUpdateUI.Views
{
    /// <summary>
    /// DownloadModsConfigView.xaml 的交互逻辑
    /// </summary>
    public partial class ModsDownloadConfigView : MetroWindow
    {
        public ModsDownloadConfigView()
        {
            InitializeComponent();

            Init();
        }

        public string ErrorMessage
        {
            set => AlertSnackbarMessage.Content = value;
        }

        private void Init()
        {
            DownloadModsConfig config = ConfigurationManager.DownloadModsConfiguration;
            DownloadDirTextBox.Text = config.DownloadDirectory;
            OwnerTextBox.Text = config.Owner;
            CodeRepoTextBox.Text = config.Repository;
            AutoDecompressTButton.IsChecked = config.IsAutoDecompress;
            DeleteFileTButton.IsChecked = config.IsDeleteFileWhenDecompress;
        }

        private void SaveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!SetNewConfig())
            {
                AlertSnackbar.IsActive = true;
                return;
            }
            ConfigurationManager.SaveConfiguration(ConfigurationManager.DownloadModsConfiguration, Constants.DownloadModsConfigPath);
            AlertSnackbar.IsActive = true;
        }

        private bool SetNewConfig()
        {
            DownloadModsConfig config = ConfigurationManager.DownloadModsConfiguration;
            string path = DownloadDirTextBox.Text.Trim();
            if (!Directory.Exists(path))
            {
                ErrorMessage = "下载目录不存在，保存失败";
                return false;
            }

            config.DownloadDirectory = path;
            config.Owner = OwnerTextBox.Text.Trim();
            config.Repository = CodeRepoTextBox.Text.Trim();
            config.IsAutoDecompress = AutoDecompressTButton.IsChecked ?? false;
            config.IsDeleteFileWhenDecompress = DeleteFileTButton.IsChecked ?? true;

            ErrorMessage = "保存成功，部分设置可能需要下次启动生效";
            return true;
        }

        private void DownloadDirTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                SelectedPath = AppDomain.CurrentDomain.BaseDirectory
            };
            System.Windows.Forms.DialogResult result = folderDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
                DownloadDirTextBox.Text = folderDialog.SelectedPath;
        }

        private void ApplyButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!SetNewConfig())
            {
                AlertSnackbar.IsActive = true;
                return;
            }
        }

        private void SnackbarMessage_ActionClick(object sender, System.Windows.RoutedEventArgs e) => AlertSnackbar.IsActive = false;
    }
}
