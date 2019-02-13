using MahApps.Metro.Controls;
using ModsUpdateUI.Configurations;
using System;
using System.IO;
using FolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;

namespace ModsUpdateUI.Views
{
    /// <summary>
    /// UpdateModsConfigView.xaml 的交互逻辑
    /// </summary>
    public partial class ModsUpdateConfigView : MetroWindow
    {
        public ModsUpdateConfigView()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            UpdateModsConfig config = ConfigurationManager.UpdateModsConfiguration;
            ModsDirTextBox.Text = config.ModsDirectory;
            OwnerTextBox.Text = config.Owner;
            CodeRepoTextBox.Text = config.Repository;
            MataInfoTextBox.Text = config.InfoFile;
            FileTypeCombpBox.Text = config.FileType;
        }

        private void ModsDirTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                SelectedPath = AppDomain.CurrentDomain.BaseDirectory
            };
            System.Windows.Forms.DialogResult result = folderDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
                ModsDirTextBox.Text = folderDialog.SelectedPath;
        }

        private void SaveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(!SetNewConfig())
            {
                AlertSnackbar.IsActive = true;
                return;
            }
            ConfigurationManager.SaveConfiguration(ConfigurationManager.UpdateModsConfiguration, Constants.UpdateModsConfigPath);
            AlertSnackbar.IsActive = true;
        }

        private void ApplyButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!SetNewConfig())
            {
                AlertSnackbar.IsActive = true;
                return;
            }
        }

        private bool SetNewConfig()
        {
            UpdateModsConfig config = ConfigurationManager.UpdateModsConfiguration;
            string path = ModsDirTextBox.Text.Trim();
            if(!Directory.Exists(path))
            {
                AlertSnackbarMessage.Content = "Mods文件夹不存在,保存失败";
                return false;
            }

            config.ModsDirectory = path;
            config.Owner = OwnerTextBox.Text.Trim();
            config.Repository = CodeRepoTextBox.Text.Trim();
            config.InfoFile = MataInfoTextBox.Text.Trim();
            config.FileType = FileTypeCombpBox.Text.Trim();

            AlertSnackbarMessage.Content = "保存成功";
            return true;
        }

        private void SnackbarMessage_ActionClick(object sender, System.Windows.RoutedEventArgs e) => AlertSnackbar.IsActive = false;
    }
}
