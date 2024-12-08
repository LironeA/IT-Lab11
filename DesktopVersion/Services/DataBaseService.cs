using DataBaseCore;
using DataBaseCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.Services
{
    public class DataBaseService : IDataBaseService
    {

        private static DataBaseController _instance;
        private DataBaseController _controller => _instance ??= new DataBaseController();

        public List<DataBase>  GetConnectedDatabases()
        {
            return _controller.ConnectedDatabases;
        }

        public DataBase CreateDatabase(string name, string path)
        {
            if(!Directory.Exists(path))
            {
                throw new Exception("Path does not exist");
            }

            var db = _controller.CreateDB(name, path);

            _controller.SaveDbToDisk(db);

            return db;
        }

        public DataBase OpenDatabase(string path)
        {
            var db = _controller.LoadDbFromDisk(path);

            return db;
        }

        public void CreateTable(DataBase db, string tableName)
        {
            _controller.CreateTable(db, new Table(tableName));
            _controller.SaveDbToDisk(db);
        }

        public void CreateColumn(DataBase db, Table tb, Column column)
        {
            _controller.CreateColumn(tb, column);
            _controller.SaveDbToDisk(db);
        }

        public void AddDataRow(DataBase db, Table table)
        {
            _controller.AddDataRow(table);
            _controller.SaveDbToDisk(db);
        }

        public void SetDataValue(DataBase db, DataValue dv, string newValue)
        {
            _controller.SetDataValue(dv, newValue);
            _controller.SaveDbToDisk(db);
        }

        public void DropDatabase(DataBase db)
        {
            _controller.DropDatabase(db);
        }

        public void DropTable(DataBase db, Table table)
        {
            _controller.DropTable(db, table);
        }
    }
}
