using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {

        public T GetService<T>()
        {
            return Application.Current.MainPage.Handler.MauiContext.Services.GetService<T>();
        }
    }
}
