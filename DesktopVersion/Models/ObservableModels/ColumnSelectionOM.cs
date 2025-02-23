using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.Models.ObservableModels
{
    public partial class ColumnSelectionOM : ObservableObject 
    {
        [ObservableProperty] public ColumnOM _column;
        [ObservableProperty] public bool _isSelected; 
    }
}
