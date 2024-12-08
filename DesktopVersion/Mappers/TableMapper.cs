using DataBaseCore.Models;
using DesktopVersion.Models.ObservableModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.Mappers
{
    public static class TableMapper
    {
        public static void Map(Table fromObj, TableOM toObj)
        {
            toObj.Id = fromObj.Id;
            toObj.Name = fromObj.Name;
            toObj.Columns = new();
            foreach(var column in fromObj.Columns)
            {
                var columnOM = new ColumnOM();
                ColumnMapper.Map(column, columnOM);
                toObj.Columns.Add(columnOM);
            }
            toObj.Data = new();
            foreach(var dataRow in fromObj.Data)
            {
                var dataRowOM = new DataRowOM();
                dataRowOM.Id = dataRow.Id;
                dataRowOM.ParentId = toObj.Id;
                dataRowOM.Values = new ObservableCollection<DataValueOM>();
                foreach(var dataRowValue in dataRow.Values)
                {
                    var dataValueOM = new DataValueOM();
                    dataValueOM.Id = dataRowValue.Id;
                    dataValueOM.ParentId = dataRowOM.Id;
                    dataValueOM.Column = toObj.Columns.FirstOrDefault(f => f.Id == dataRowValue.Column.Id);
                    dataValueOM.Value = dataRowValue.Value;
                    dataRowOM.Values.Add(dataValueOM);
                }
                toObj.Data.Add(dataRowOM);
            }
        }

        public static void Map(TableOM fromObj, Table toObj)
        {
            toObj.Id = fromObj.Id;
            toObj.Name = fromObj.Name;
        }
    }
}
