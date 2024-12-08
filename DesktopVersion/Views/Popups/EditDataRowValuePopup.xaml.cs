using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopVersion.ViewModels;
using Mopups.Pages;

namespace DesktopVersion.Views.Popups;

public partial class EditDataRowValuePopup : PopupPage
{
    private readonly EditDataRowValuePopupVM _vm;

    public EditDataRowValuePopup(EditDataRowValuePopupVM vm)
    {
        _vm = vm;
        this.BindingContext = _vm;
        InitializeComponent();
    }

    
}