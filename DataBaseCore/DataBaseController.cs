using DataBaseCore.Models;

namespace DataBaseCore
{
    public class DataBaseController
    {
        private int _idCounter = 0;
        public List<DataBase> ConnectedDatabases { get; set; }


        public DataBaseController()
        {
            ConnectedDatabases = new List<DataBase>();
        }

        public DataBase CreateDB(string name, string path)
        {
            var db = new DataBase(){ Name = name, RootDirectoryPath = path };
            db.Id = _idCounter++;
            ConnectedDatabases.Add(db);
            return db;
        }

        public void SaveDbToDisk(DataBase db)
        {
            SaveManger.Save(db);
        }

        public DataBase LoadDbFromDisk(string path)
        {
            var db = SaveManger.Load(path);
            if(db is null)
            {
                return null;
            }


            ConnectedDatabases.Add(db);
            db.Id = _idCounter++;
            foreach(var tb in db.Tables)
            {
                tb.Id = _idCounter++;
                foreach(var dataRow in tb.Data)
                {
                    dataRow.Id = _idCounter++;
                    foreach(var dataValue in dataRow.Values)
                    {
                        dataValue.Id = _idCounter++;
                    }
                }
            }
            return db;
        }
        public void CreateTable(DataBase db, Table tb)
        {
            tb.Id = _idCounter++;
            db.AddTable(tb);
        }

        public void CreateColumn(Table tb, Column column)
        {
            column.Id = _idCounter++;
            tb.AddColumn(column);
        }

        public void AddDataRow(Table table)
        {
            var dataRow = new DataRow();
            dataRow.Id = _idCounter++;
            dataRow.Values = new List<DataValue>();
            foreach(var column in table.Columns)
            {
                dataRow.Values.Add(new DataValue { Id = _idCounter++, Column = column, Value = column.DefaultValue });
            }
            table.AddData(dataRow);
        }

        public void SetDataValue(DataValue dv, string newValue)
        {
            var valueObj = dv.Column.Parse(newValue);
            if(valueObj is null)
            {
                throw new Exception($"Error parsing value \"{newValue}\" as \"{dv.Column.Type.ToString()}\"");
            }
            dv.Value = valueObj;
        }

        public void DropDatabase(DataBase db)
        {
            ConnectedDatabases.Remove(db);
            SaveManger.Delete(db.FolderPath);
        }

        public void DropTable(DataBase db, Table table)
        {
            SaveManger.DeleteTable(db.FolderPath, table.Name);
        }

        public Table TableSelection(DataBase db, Table table, List<Column> columns)
        {
            throw new NotImplementedException();
        }
    }
}
