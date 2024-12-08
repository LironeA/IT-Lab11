using CommunityToolkit.Mvvm.ComponentModel;
using DataBaseCore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.Models.ObservableModels
{
    public partial class TableOM : ObservableObject
    {
        [ObservableProperty] public int _id;
        [ObservableProperty] public int _parentId;
        [ObservableProperty] private string _name;
        [ObservableProperty] private ObservableCollection<ColumnOM> _columns;
        [ObservableProperty] private ObservableCollection<DataRowOM> _data;
    }
}
