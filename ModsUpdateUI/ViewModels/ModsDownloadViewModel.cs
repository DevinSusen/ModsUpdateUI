using ModsUpdateUI.Models;
using ModsUpdateUI.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.IO;
using System;
using ModsUpdateUI.Configurations;
using MaterialDesignThemes.Wpf;
using System.IO.Compression;

namespace ModsUpdateUI.ViewModels
{
    public class ModsDownloadViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DownloadModItem> _modItems;
        public ObservableCollection<DownloadModItem> ModItems
        {
            get => _modItems;
            private set
            {
                _modItems = value;
                OnPropertyChanged("ModItems");
            }
        }

        private ISnackbarMessageQueue _snackbarMessQueue;

        private string _fileTypeFilter;
        public string FileTypeFilter
        {
            get => _fileTypeFilter;
            set
            {
                _fileTypeFilter = value;
                OnPropertyChanged("FileTypeFilter");
                ShowByFileType(value);
            }
        }

        private List<string> _fileTypes;
        public List<string> FileTypes
        {
            get => _fileTypes;
            set
            {
                _fileTypes = value;
                OnPropertyChanged("FileTypes");
            }
        }

        private List<DownloadModItem> _downloadItems;
        public List<DownloadModItem> DownloadItems
        {
            get => _downloadItems;
            set
            {
                _downloadItems = value;
                OnPropertyChanged("DownloadItems");
            }
        }

        private int _leavingCount;
        public int LeavingCount
        {
            get => _leavingCount;
            set
            {
                _leavingCount = value;
                OnPropertyChanged("LeavingCount");
            }
        }

        private int _downloadedCount;
        public int DownloadedCount
        {
            get => _downloadedCount;
            set
            {
                _downloadedCount = value;
                OnPropertyChanged("DownloadedCount");
            }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged("Message");
                _snackbarMessQueue.Enqueue(value);
            }
        }

        #region Download
        private System.Net.WebClient _client;
        private System.Net.WebClient Client
        {
            get
            {
                if (_client == null)
                    _client = new System.Net.WebClient();
                return _client;
            }
        }

        public void DownloadMod()
        {
            DownloadedCount = 0;
            LeavingCount = DownloadItems.Count;
            if (LeavingCount > 0)
            {
                Client.DownloadFileCompleted += Client_DownloadFileCompleted;
                Client.DownloadFileAsync(new Uri(DownloadItems[0].ModInfo.BrowserDownloadUrl),
                    ConfigurationManager.DownloadModsConfiguration.DownloadDirectory + Path.DirectorySeparatorChar + DownloadItems[0].ModInfo.Name);
            }
            else Message = "未选择待下载项目";
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (DownloadItems[0].ModInfo.ContentType == Constants.ZipType)
            {
                string path = ConfigurationManager.DownloadModsConfiguration.DownloadDirectory + Path.DirectorySeparatorChar + DownloadItems[0].ModInfo.Name;
                DecompressFile(path, ConfigurationManager.DownloadModsConfiguration.IsDeleteFileWhenDecompress);
            }
            DownloadItems.RemoveAt(0);
            DownloadedCount += 1;
            LeavingCount -= 1;
            if (LeavingCount > 0)
            {
                Client.DownloadFileAsync(new Uri(DownloadItems[0].ModInfo.BrowserDownloadUrl), 
                    ConfigurationManager.DownloadModsConfiguration.DownloadDirectory + Path.DirectorySeparatorChar + DownloadItems[0].ModInfo.Name);
            }
            else
            {
                Message = "下载完毕";
                Client.DownloadFileCompleted -= Client_DownloadFileCompleted;
            }
        }

        private void DecompressFile(string filePath, bool canDelete)
        {
            ZipFile.ExtractToDirectory(filePath, ConfigurationManager.DownloadModsConfiguration.DownloadDirectory);
            if (canDelete)
                File.Delete(filePath);
        }

        #endregion

        #region Show

        public void ShowAll()
        {
            foreach (var i in ModItems)
                i.Visibility = true;
        }

        public void ShowUndownloaded()
        {
            foreach (var i in ModItems)
                i.Visibility = !i.IsExists;
        }

        public void ShowDownloaded()
        {
            foreach (var i in ModItems)
                i.Visibility = i.IsExists;
        }

        public void ShowByFileType(string fileType)
        {
            if (fileType == "All")
                ShowAll();
            else
            {
                foreach(var i in ModItems)
                {
                    i.Visibility = false;
                    if (i.ModInfo.ContentType == fileType)
                        i.Visibility = true;
                }
            }
        }

        #endregion

        #region Constructor

        public ModsDownloadViewModel(ISnackbarMessageQueue messageQueue)
        {
            _snackbarMessQueue = messageQueue ?? throw new ArgumentNullException(nameof(messageQueue));
            InitAsync();
        }

        private async void InitAsync()
        {
            var result = await ServiceManager.GithubService.GetLastestModsAsync();
            ModItems = DownloadModItem.FromList(result);
            FileTypes = new List<string>(result.Select(m => m.ContentType).Distinct())
            {
                "All"
            };
            FileTypeFilter = "All";
            Message = "数据加载完成";
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public partial class DownloadModItem : INotifyPropertyChanged
    {
        private RemoteModInfo _modInfo;
        public RemoteModInfo ModInfo
        {
            get => _modInfo;
            set
            {
                _modInfo = value;
                OnPropertyChanged("ModInfo");
            }
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

        private bool _isExisting;
        public bool IsExists
        {
            get => _isExisting;
            set
            {
                _isExisting = value;
                OnPropertyChanged("IsExists");
            }
        }

        private bool _visibility;
        public bool Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public partial class DownloadModItem
    {
        public static ObservableCollection<DownloadModItem> FromList(List<RemoteModInfo> items)
        {
            ObservableCollection<DownloadModItem> downloadMods = new ObservableCollection<DownloadModItem>();
            foreach(var i in items)
            {
                downloadMods.Add(new DownloadModItem
                {
                    ModInfo = i,
                    IsChecked = false,
                    Visibility = true,
                    IsExists = false
                });
            }
            return downloadMods;
        }
    }
}
