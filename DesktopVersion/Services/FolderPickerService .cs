using CommunityToolkit.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.Services
{
    public class FolderPickerService : IFolderPickerService
    {
        public async Task<string?> PickFolderAsync()
        {
            try
            {
                var folderPicker = FolderPicker.Default;
                var folder = await folderPicker.PickAsync();

                if(folder != null)
                {
                    return folder.Folder.Path;
                }
            }
            catch(Exception ex)
            {
                // Логування помилки
                Console.WriteLine($"Error picking folder: {ex.Message}");
            }

            return null;
        }
    }
}
