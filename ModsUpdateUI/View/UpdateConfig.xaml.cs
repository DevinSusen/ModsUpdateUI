using System.Windows;
using System.Windows.Forms;

namespace ModsUpdateUI.View
{
    /// <summary>
    /// UpdateConfig.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateConfig : Window
    {
        public UpdateConfig()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            CheckUpdateConfig config = Config.UpdateConfig;
            OwnerText.Text = config.Owner;
            ReposText.Text = config.Repository;
            FileTypeSelect.Text = config.FileType;
            ModsText.Text = config.ModsDir;
            InfoFileTextBox.Text = config.InfoFile;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string ownerName = OwnerText.Text.Trim();
            string repos = ReposText.Text.Trim();
            string fileType = FileTypeSelect.Text.Trim();
            string modsPath = ModsText.Text.Trim();
            string infoF = InfoFileTextBox.Text.Trim();
            CheckUpdateConfig config = new CheckUpdateConfig(modsPath, repos, ownerName, fileType, infoF);
            ConfigLoader.SaveCheckUpdateConfig(config);
            Config.UpdateConfig = config;
            System.Windows.MessageBox.Show("保存成功");
        }

        private void ModsText_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory
            };
            DialogResult result = folderDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
                ModsText.Text = folderDialog.SelectedPath;
        }
    }
}
