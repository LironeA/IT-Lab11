using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.Services
{
    public interface IFolderPickerService
    {
        Task<string?> PickFolderAsync();
    }

}
