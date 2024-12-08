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
    public partial class DatabaseOM : ObservableObject
    {
        [ObservableProperty] public int _id;
        [ObservableProperty] public string _name;
        [ObservableProperty] public string _rootDirectoryPath;
        [ObservableProperty] public string folderPath;

        [ObservableProperty] public ObservableCollection<TableOM> _tables;
    }
}
