using DesktopVersion.ViewModels;
using Mopups.Pages;

namespace DesktopVersion.Views.Popups;

public partial class ColumnSelectionPopup : PopupPage
{
    private readonly ColumnSelectionPopupVM _vm;

    public ColumnSelectionPopup(ColumnSelectionPopupVM vm)
    {
        _vm = vm;
        this.BindingContext = _vm;
        InitializeComponent();
    }
}