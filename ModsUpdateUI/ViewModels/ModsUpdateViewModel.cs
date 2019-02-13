using ModsUpdateUI.Configurations;
using ModsUpdateUI.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ModsUpdateUI.Services;

namespace ModsUpdateUI.ViewModels
{
    public class ModsUpdateViewModel : INotifyPropertyChanged
    {
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
    }
}
