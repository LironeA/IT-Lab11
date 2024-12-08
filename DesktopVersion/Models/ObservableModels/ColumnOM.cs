using CommunityToolkit.Mvvm.ComponentModel;
using DataBaseCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.Models.ObservableModels
{
    public partial class ColumnOM : ObservableObject
    {
        [ObservableProperty] public int _id;
        [ObservableProperty] public bool _isPrimaryKey;
        [ObservableProperty] public string _name;
        [ObservableProperty] public bool _nullable;
        [ObservableProperty] public string _defaultValue;
        [ObservableProperty] public ColumnType _type;
    }
}
