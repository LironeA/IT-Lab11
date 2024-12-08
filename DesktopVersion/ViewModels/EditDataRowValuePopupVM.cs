using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class EditDataRowValuePopupVM : BaseViewModel
    {
        private readonly IPopupNavigation _popupNaviagtion;
        private readonly EditDataRowValueDTO _dto;

        [ObservableProperty] private string _value;

        public EditDataRowValuePopupVM(EditDataRowValueDTO dto, IPopupNavigation popupNavigation)
        {
            _popupNaviagtion = popupNavigation;
            _dto = dto;
            Value = _dto.DataValueOM.Value.ToString();
        }

        [RelayCommand]
        public async Task Save()
        {
            if(Value.Equals(_dto.DataValueOM.Value.ToString()))
            {
                return;
            }

            var result = new EditDataRowValuePopupResult();
            result.DataValueOM = _dto.DataValueOM;
            result.NewValue = Value;

            if(_dto.EditDataRowValueCallback is not null)
            {
                _dto.EditDataRowValueCallback.Execute(result);
            }

            await _popupNaviagtion.PopAsync();
        }
    }
}
