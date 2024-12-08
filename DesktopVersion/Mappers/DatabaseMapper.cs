using DataBaseCore.Models;
using DesktopVersion.Models.ObservableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.Mappers
{
    public static class DatabaseMapper
    {
        public static void Map(DataBase fromObj, DatabaseOM toObj)
        {
            toObj.Id = fromObj.Id;
            toObj.Name = fromObj.Name;
            toObj.RootDirectoryPath = fromObj.RootDirectoryPath;
            toObj.FolderPath = fromObj.FolderPath;

        }

        public static void Map(DatabaseOM fromObj, DataBase toObj)
        {
            toObj.Id = fromObj.Id;
            toObj.Name = fromObj.Name;
            toObj.RootDirectoryPath = fromObj.RootDirectoryPath;
        }


    }
}
