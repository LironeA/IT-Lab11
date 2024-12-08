using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataBaseCore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.Models.ObservableModels
{
    public partial class DataRowOM : ObservableObject
    {
        [ObservableProperty] public int _id;
        [ObservableProperty] public int _parentId;
        [ObservableProperty] private ObservableCollection<DataValueOM> _values;
        [ObservableProperty] private IRelayCommand _editValueCommand;

    }

    public partial class DataValueOM : ObservableObject
    {
        [ObservableProperty] public int _id;
        [ObservableProperty] public int _parentId;
        public ColumnOM Column { get; set; }
        public object Value { get; set; }
    }
}
