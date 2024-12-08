using CommunityToolkit.Mvvm.Input;
using DesktopVersion.Models.ObservableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.Models.DTO
{
    public class EditDataRowValueDTO
    {
        public IRelayCommand EditDataRowValueCallback { get; set; }
        public DataValueOM DataValueOM { get; set; }
    }
}
