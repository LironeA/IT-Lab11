using CommunityToolkit.Maui.Views;
using DesktopVersion.Services;
using DesktopVersion.ViewModels;
using Mopups.Pages;

namespace DesktopVersion.Views.Popups;

public partial class CreateDatabasePopup : PopupPage
{
    private readonly CreateDatabasePopupVM _vm;
    public CreateDatabasePopup(CreateDatabasePopupVM vm)
    {
        _vm = vm;
        this.BindingContext = _vm;
        InitializeComponent();
    }
}