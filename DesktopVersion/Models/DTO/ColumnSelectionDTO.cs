using CommunityToolkit.Mvvm.Input;
using DesktopVersion.Models.ObservableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.Models.DTO
{
    public class ColumnSelectionDTO
    {
        public TableOM Table { get; set; } 
        public IRelayCommand ColumnSelectionCallBack { get; set; }
    }
}
