using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataBaseCore.Models;
using DesktopVersion.Models;
using DesktopVersion.Models.DTO;
using Mopups.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.ViewModels
{
    public partial class CreateColumnPopupVM : BaseViewModel
    {
        private readonly IPopupNavigation _popupNaviagtion;

        private readonly CreateColumnDTO _dto;

        [ObservableProperty] public string _name;
        [ObservableProperty] public bool _isPrimaryKey;
        [ObservableProperty] public bool _nullable;
        [ObservableProperty] public string _defaultValue;
        [ObservableProperty] public ColumnType _type;
        [ObservableProperty] public List<ColumnType> _columnTypes = Enum.GetValues<ColumnType>().ToList();
        public CreateColumnPopupVM(CreateColumnDTO dto, IPopupNavigation popupNavigation)
        {
            _dto = dto;
            _popupNaviagtion = popupNavigation;
        }

        [RelayCommand]
        public async Task Create()
        {
            if(_dto.CreateColumnCallback is not null)
            {
                _dto.CreateColumnCallback.Execute(new CreateColumnPopupResult() { Name = Name, IsPrimaryKey = IsPrimaryKey, Nullable = Nullable, DefaultValue = DefaultValue, Type = Type });
            }
            await _popupNaviagtion.PopAsync();
        }
    }
}
