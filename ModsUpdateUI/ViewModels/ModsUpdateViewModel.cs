using ModsUpdateUI.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ModsUpdateUI.Services;
using System.Linq;
using System.Threading.Tasks;
using System;
using ModsUpdateUI.Configurations;
using System.IO;
using System.IO.Compression;
using MaterialDesignThemes.Wpf;

namespace ModsUpdateUI.ViewModels
{
    public class ModsUpdateViewModel : INotifyPropertyChanged
    {
        private ISnackbarMessageQueue _snackbarMessQueue;

        public void ShowUpdatableMods()
        {
            foreach (var i in ModItems)
                i.Visibility = i.CanUpdate;
        }

        public void ShowAllMods()
        {
            foreach (var i in ModItems)
                i.Visibility = true;
        }

        private bool _isChecking = true;
        public bool IsChecking
        {
            get => _isChecking;
            set
            {
                _isChecking = value;
                OnPropertyChanged("IsChecking");
            }
        }

        public async void CheckUpdateAsync()
        {
            var result = await _remoteMods;
            var lists = result.Where(v => v.ContentType == Constants.ZipType);
            ChangeToRemoteItems(lists);
            foreach (var i in ModItems)
               i.CanUpdate = i.CheckUpdate(lists);
            IsChecking = false;
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

        private int _updateCount;
        public int UpdateCount
        {
            get => _updateCount;
            set
            {
                _updateCount = value;
                OnPropertyChanged("UpdateCount");
            }
        }

        private int _updatedCount;
        public int UpdatedCount
        {
            get => _updatedCount;
            set
            {
                _updatedCount = value;
                OnPropertyChanged("UpdatedCount");
            }
        }

        private List<RemoteModInfo> _map;
        private List<UpdateModItem> _updateItems;
        public List<UpdateModItem> UpdateItems
        {
            get => _updateItems;
            set
            {
                _updateItems = value;
                OnPropertyChanged("UpdateItems");
                _map = new List<RemoteModInfo>();
                foreach (var i in value)
                {
                    var it = _mappingRemote.First(v => v.Name.StartsWith(i.ModInfo.Id));
                    _map.Add(it);
                }
            }
        }

        private List<RemoteModInfo> _mappingRemote = new List<RemoteModInfo>();

        private void ChangeToRemoteItems(IEnumerable<RemoteModInfo> lists)
        {
            foreach(var i in ModItems)
            {
                var it = lists.FirstOrDefault(v => v.Name.StartsWith(i.ModInfo.Id));
                if (it == default(RemoteModInfo))
                    continue;
                _mappingRemote.Add(it);
            }
        }

        public void UpdateMod()
        {
            UpdatedCount = 0;
            UpdateCount = UpdateItems.Count;
            if (UpdateCount > 0)
            {
                Client.DownloadFileCompleted += Client_DownloadFileCompleted;
                Client.DownloadFileAsync(new Uri(_map[0].BrowserDownloadUrl),
                    ConfigurationManager.UpdateModsConfiguration.ModsDirectory + Path.DirectorySeparatorChar + _map[0].Name);
            }
            else Message = "未选择待更新项目";
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string prefix = ConfigurationManager.UpdateModsConfiguration.ModsDirectory + Path.DirectorySeparatorChar;
            string name = _map[0].Name.Substring(0, _map[0].Name.LastIndexOf('-'));
            Directory.Delete(prefix + name, true);
            DecompressFile(prefix + _map[0].Name);
            _map.RemoveAt(0);
            UpdatedCount += 1;
            UpdateCount -= 1;
            if (UpdateCount > 0)
            {
                Client.DownloadFileAsync(new Uri(_map[0].BrowserDownloadUrl),
                    ConfigurationManager.UpdateModsConfiguration.ModsDirectory + Path.DirectorySeparatorChar + _map[0].Name);
            }
            else
            {
                Message = "更新完毕";
                Client.DownloadFileCompleted -= Client_DownloadFileCompleted;
            }
        }

        private void DecompressFile(string filePath)
        {
            ZipFile.ExtractToDirectory(filePath, ConfigurationManager.UpdateModsConfiguration.ModsDirectory);
            File.Delete(filePath);
        }

        private Task<List<RemoteModInfo>> _remoteMods;

        public ModsUpdateViewModel(ISnackbarMessageQueue messageQueue)
        {
            _snackbarMessQueue = messageQueue ?? throw new ArgumentNullException(nameof(messageQueue));
            InitAsync();
        }

        public void InitAsync()
        {
            GithubService service = new GithubService(ConfigurationManager.UpdateModsConfiguration.Owner, ConfigurationManager.UpdateModsConfiguration.Repository);
            _remoteMods = service.GetLastestModsAsync();
            Message = "数据加载完成";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<UpdateModItem> ModItems { get; } = UpdateModItem.FromLocalModInfo(ServiceManager.LocalService.FromDirectory());

        public void OnPropertyChanged([CallerMemberName]string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    public partial class UpdateModItem : INotifyPropertyChanged
    {
        private LocalModInfo _modInfo;
        public LocalModInfo ModInfo
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

        private bool _canUpdate;
        public bool CanUpdate
        {
            get => _canUpdate;
            set
            {
                _canUpdate = value;
                OnPropertyChanged("CanUpdate");
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

    public partial class UpdateModItem
    {
        public static ObservableCollection<UpdateModItem> FromLocalModInfo(List<LocalModInfo> infos)
        {
            ObservableCollection<UpdateModItem> items = new ObservableCollection<UpdateModItem>();
            foreach (var i in infos)
            {
                items.Add(new UpdateModItem
                {
                    ModInfo = i,
                    IsChecked = false,
                    CanUpdate = false,
                    Visibility = true
                });
            }
            return items;
        }

        public bool CheckUpdate(IEnumerable<RemoteModInfo> infos)
        {
            var it = infos.FirstOrDefault(v => v.Name.StartsWith(ModInfo.Id));
            if (it == default(RemoteModInfo))
                return false;
            if (it.Version != ModInfo.Version)
                return true;
            return false;
        }
    }
}
