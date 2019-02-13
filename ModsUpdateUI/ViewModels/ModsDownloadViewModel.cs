using ModsUpdateUI.Models;
using ModsUpdateUI.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;

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

        public event PropertyChangedEventHandler PropertyChanged;

        public ModsDownloadViewModel() => InitAsync();

        private async void InitAsync()
        {
            var result = await ServiceManager.GithubService.GetLastestModsAsync();
            ModItems = DownloadModItem.FromList(result);
            FileTypes = new List<string>(result.Select(m => m.ContentType).Distinct())
            {
                "All"
            };
            FileTypeFilter = "All";
        }

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
