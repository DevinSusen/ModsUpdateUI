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
using System.Threading.Tasks;

namespace ModsUpdateUI.ViewModels
{
    public class ModsDownloadViewModel : INotifyPropertyChanged
    {
        private List<DownloadModItem> _allItems;
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

        private readonly ISnackbarMessageQueue _snackbarMessQueue;

        private string _fileTypeFilter;
        public string FileTypeFilter
        {
            get => _fileTypeFilter;
            set
            {
                _fileTypeFilter = value;
                OnPropertyChanged("FileTypeFilter");
                GetListByFileType(value);
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

        public async Task DownloadAsync()
        {
            await Task.Run(() => DownloadMod());
        }

        private void DownloadMod()
        {
            DownloadedCount = 0;
            LeavingCount = DownloadItems.Count;
            if (LeavingCount > 0)
            {
                Client.DownloadFileCompleted += Client_DownloadFileCompleted;
                Client.DownloadFileAsync(new Uri(DownloadItems[0].ModInfo.BrowserDownloadUrl),
                    ConfigurationManager.DownloadModsConfiguration.DownloadDirectory + Path.DirectorySeparatorChar + DownloadItems[0].ModInfo.Name);
                while(Client.IsBusy);
            }
            else Message = "未选择待下载项目";
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (ConfigurationManager.DownloadModsConfiguration.IsAutoDecompress)
            {
                if (DownloadItems[0].ModInfo.ContentType == Constants.ZipType)
                {
                    string path = ConfigurationManager.DownloadModsConfiguration.DownloadDirectory + Path.DirectorySeparatorChar + DownloadItems[0].ModInfo.Name;
                    DecompressFile(path, ConfigurationManager.DownloadModsConfiguration.IsDeleteFileWhenDecompress);
                }
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
            try
            {
                ZipFile.ExtractToDirectory(filePath, ConfigurationManager.DownloadModsConfiguration.DownloadDirectory);
            }
            catch (Exception)
            {
                Message = "出现错误，请检查您的网络，并重新尝试。";
                if(File.Exists(filePath))
                    File.Delete(filePath);
                return;
            }
            
            if (canDelete)
                File.Delete(filePath);
        }

        #endregion

        #region Show

        public void GetAll()
        {
            ModItems = new ObservableCollection<DownloadModItem>(_allItems);
        }

        public void GetUndownloaded()
        {
            var result = from mod in _allItems
                         where !mod.IsExists
                         select mod;
            ModItems = new ObservableCollection<DownloadModItem>(result);
        }

        public void GetDownloaded()
        {
            var result = from mod in _allItems
                         where mod.IsExists
                         select mod;
            ModItems = new ObservableCollection<DownloadModItem>(result);
        }

        public void GetListByFileType(string fileType)
        {
            if (fileType == "All")
                GetAll();
            else
            {
                var result = from mod in _allItems
                             where mod.ModInfo.ContentType == fileType
                             select mod;
                ModItems = new ObservableCollection<DownloadModItem>(result);
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
            _allItems = DownloadModItem.FromRemoteList(result);
            ModItems = new ObservableCollection<DownloadModItem>(_allItems);
            
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

        //private bool _visibility;
        //public bool Visibility
        //{
        //    get => _visibility;
        //    set
        //    {
        //        _visibility = value;
        //        OnPropertyChanged("Visibility");
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public partial class DownloadModItem
    {
        public static List<DownloadModItem> FromRemoteList(List<RemoteModInfo> items)
        {
            List<DownloadModItem> downloadMods = new List<DownloadModItem>();
            LocalService ls = new LocalService(ConfigurationManager.UpdateModsConfiguration.ModsDirectory, ConfigurationManager.UpdateModsConfiguration.InfoFile);
            var lss = ls.FromDirectory();
            foreach(var i in items)
            {
                DownloadModItem item = new DownloadModItem
                {
                    ModInfo = i,
                    IsChecked = false
                };
                downloadMods.Add(item);
                string fileName = Path.GetFileNameWithoutExtension(i.Name);
                int idx = fileName.IndexOf('-');
                if (idx == -1)
                    continue;
                var name = fileName.Substring(0, idx);
                if (lss.Exists(v => v.Id == name))
                    item.IsExists = true;
                else item.IsExists = false;
            }
            return downloadMods;
        }
    }
}
