using DesktopVersion.ViewModels;
using Mopups.Pages;

namespace DesktopVersion.Views.Popups;

public partial class CreateColumnPopup : PopupPage
{
    private readonly CreateColumnPopupVM _vm;
    public CreateColumnPopup(CreateColumnPopupVM vm)
    {
        _vm = vm;
        this.BindingContext = _vm;
        InitializeComponent();
    }
}