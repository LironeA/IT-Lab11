using DataBaseCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.Services
{
    public interface IDataBaseService
    {
        void DropDatabase(DataBase db);
        void SetDataValue(DataBase db, DataValue dv, string newValue);
        void CreateColumn(DataBase db, Table tb, Column column);
        void CreateTable(DataBase db, string tableName);
        DataBase OpenDatabase(string path);
        DataBase CreateDatabase(string name, string path);
        List<DataBase> GetConnectedDatabases();
        void AddDataRow(DataBase db, Table table);
        void DropTable(DataBase db, Table table);
    }
}
