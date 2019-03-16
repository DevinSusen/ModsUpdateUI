using MahApps.Metro.Controls;
using ModsUpdateUI.Configurations;
using ModsUpdateUI.Services;
using ModsUpdateUI.Views;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

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

            CheckUpdateAsync();
        }

        private async void CheckUpdateAsync()
        {
            if (await CanUpdate())
            {
                var result = MessageBox.Show("有更新，是否下载？", "检查更新", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Process.Start(@"https://github.com/DevinSusen/ModsUpdateUI/releases");
                }
            }
        }

        private async Task<bool> CanUpdate()
        {
            ApplicationService service = new ApplicationService();
            return await service.CheckUpdateAsync();
        }

        private async void CheckUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if(await CanUpdate())
            {
                var result = MessageBox.Show("有更新，是否下载？", "检查更新", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Process.Start(@"https://github.com/DevinSusen/ModsUpdateUI/releases");
                }
            }
            else
            {
                MessageBox.Show("当前已是最新版本", "检查更新");
            }
        }

        private void WikiButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(@"https://github.com/DevinSusen/ModsUpdateUI");
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AboutView view = new AboutView();
            view.Show();
        }

        private void ModsDownloadConfigButton_Click(object sender, RoutedEventArgs e)
        {
            ModsDownloadConfigView view = new ModsDownloadConfigView();
            view.Show();
        }

        private void ModsUpdateConfigButton_Click(object sender, RoutedEventArgs e)
        {
            ModsUpdateConfigView view = new ModsUpdateConfigView();
            view.Show();
        }

        private void DownloadModsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ConfigurationManager.DownloadModsConfiguration.IsOK)
                return;
            ModsDownloadView view = new ModsDownloadView();
            view.Show();
        }

        private void UpdateModsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ConfigurationManager.UpdateModsConfiguration.IsOK)
                return;
            ModsUpdateView view = new ModsUpdateView();
            view.Show();
        }

        //private void PackButton_Click(object sender, RoutedEventArgs e)
        //{
        //    string email = EmailTextBox.Text.Trim();
        //    string password = EmailPasswordBox.Password;
        //    if (!IsValidEmail(email))
        //    {
        //        //TODO: Message
        //        return;
        //    }
        //    string path = CompressSaveFiles(Utils.Utilities.GetTheScrollOfTaiwuSaveFilesFolder());
        //    MailMessage mail = new MailMessage(email, email);
        //    mail.Subject = "存档";
        //    mail.SubjectEncoding = Encoding.UTF8;
        //    mail.Attachments.Add(new Attachment(path));

        //    SmtpClient client = new SmtpClient();
        //}

        //private bool IsValidEmail(string email)
        //{
        //    try
        //    {
        //        var addr = new MailAddress(email);
        //        return addr.Address == email;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //private string CompressSaveFiles(string filePath)
        //{
        //    Process proc = new Process();
        //    string basePath = new DirectoryInfo("..").FullName + Path.DirectorySeparatorChar + @"tools";
        //    string exePath = basePath + @"\7za.exe";
        //    proc.StartInfo.FileName = exePath;
        //    string compFile = basePath + @"\Save.7z";
        //    proc.StartInfo.Arguments = "a " + compFile + " " + filePath;
        //    proc.StartInfo.CreateNoWindow = false;
        //    proc.Start();
        //    proc.WaitForExit();
        //    proc.Close();
        //    return compFile;
        //}
    }
}
