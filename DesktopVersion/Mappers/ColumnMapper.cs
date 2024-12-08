using DataBaseCore.Models;
using DesktopVersion.Models.ObservableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.Mappers
{
    public static class ColumnMapper
    {
        public static void Map(Column fromObj, ColumnOM toObj)
        {
            toObj.Id = fromObj.Id;
            toObj.IsPrimaryKey = fromObj.IsPrimaryKey;
            toObj.Name = fromObj.Name;
            toObj.Nullable = fromObj.Nullable;
            toObj.DefaultValue = fromObj.DefaultValue;
            toObj.Type = fromObj.Type;
        }

        public static void Map(ColumnOM fromObj, Column toObj)
        {
            toObj.Id = fromObj.Id;
            toObj.IsPrimaryKey = fromObj.IsPrimaryKey;
            toObj.Name = fromObj.Name;
            toObj.Nullable = fromObj.Nullable;
            toObj.DefaultValue = fromObj.DefaultValue;
            toObj.Type = fromObj.Type;
        }
    }
}
