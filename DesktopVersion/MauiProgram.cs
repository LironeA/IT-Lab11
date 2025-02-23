using CommunityToolkit.Maui;
using DesktopVersion.Models.DTO;
using DesktopVersion.Services;
using DesktopVersion.ViewModels;
using DesktopVersion.Views;
using DesktopVersion.Views.Popups;
using Microsoft.Extensions.Logging;
using Mopups.Hosting;
using Mopups.Interfaces;
using Mopups.Services;
using UraniumUI;

namespace DesktopVersion
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                      .UseMauiApp<App>()
                      .UseMauiCommunityToolkit()
                      .UseUraniumUI()
                      .UseUraniumUIMaterial()
                      .ConfigureMopups()
                      .ConfigureFonts(fonts =>
                      {
                          fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                          fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                      });

            builder.Services.AddSingleton<IPopupNavigation>(MopupService.Instance);
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<CreateDatabasePopup>();
            builder.Services.AddTransient<CreateDatabasePopupVM>();
            builder.Services.AddTransient<CreateColumnPopup>();
            builder.Services.AddTransient<CreateColumnPopupVM>();
            builder.Services.AddTransient<EditDataRowValuePopup>();
            builder.Services.AddTransient<EditDataRowValuePopupVM>();
            builder.Services.AddTransient<ColumnSelectionPopup>();
            builder.Services.AddTransient<ColumnSelectionPopupVM>();


            builder.Services.AddTransient<IDataBaseService, DataBaseService>();
            builder.Services.AddSingleton<IFolderPickerService, FolderPickerService>();

            builder.Services.AddSingleton<CreateDatabaseDTO>();
            builder.Services.AddSingleton<CreateColumnDTO>();
            builder.Services.AddSingleton<EditDataRowValueDTO>();
            builder.Services.AddSingleton<ColumnSelectionDTO>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
