using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopVersion.Models;
using DesktopVersion.Models.DTO;
using DesktopVersion.Services;
using Mopups.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.ViewModels
{
    public partial class CreateDatabasePopupVM : BaseViewModel
    {
        private readonly CreateDatabaseDTO _dto;
        private readonly IFolderPickerService _folderPickerService;
        private readonly IPopupNavigation _popupNaviagtion;


        [ObservableProperty] private string _databaseName;
        [ObservableProperty] private string _databasePath;

        public CreateDatabasePopupVM(CreateDatabaseDTO dto, IFolderPickerService folderPickerService, IPopupNavigation popupNavigation)
        {
            _dto = dto;
            _folderPickerService = folderPickerService;
            _popupNaviagtion = popupNavigation;
        }

        [RelayCommand]
        public async Task Create()
        {
            if(_dto.CreateCallBack is not null)
            {
                _dto.CreateCallBack.Execute(new CreateDatabasePopupResult() { Name = DatabaseName, Path = DatabasePath });
            }

            await _popupNaviagtion.PopAsync();
        }

        [RelayCommand]
        public async Task Browse()
        {
            var path = await _folderPickerService.PickFolderAsync();
            if(path != null)
            {
                DatabasePath = path;
            }
        }
    }
}
