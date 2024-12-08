using DataBaseCore;
using DataBaseCore.Models;

namespace UnitTests
{
    public class Tests
    {
        DataBaseController dbController;

        [SetUp]
        public void Setup()
        {
            dbController = new DataBaseController();
        }

        [TearDown]
        public void TearDown()
        {
            dbController = null;
            if(Directory.Exists("D:\\CSC\\4th\\IT\\Lab1\\TestDB"))
            { 
                Directory.Delete("D:\\CSC\\4th\\IT\\Lab1\\TestDB", true);
            }
        }

        [Test]
        public void CreateDBObjectAndAddToList()
        {
            var db = dbController.CreateDB("TestDB", "D:\\CSC\\4th\\IT\\Lab1");
            Assert.AreEqual("TestDB", db.Name);
            Assert.AreEqual("D:\\CSC\\4th\\IT\\Lab1", db.RootDirectoryPath);
            Assert.IsTrue(dbController.ConnectedDatabases.Any(d => d.Name == "TestDB"));
        }

        [Test]
        public void CreateDBAndSave()
        {
            var db = dbController.CreateDB("TestDB", "D:\\CSC\\4th\\IT\\Lab1");
            dbController.SaveDbToDisk(db);
            Assert.IsTrue(Directory.Exists(Path.Combine(db.RootDirectoryPath, db.Name)));
            Assert.IsTrue(File.Exists(Path.Combine(db.FolderPath, db.Name + ".db")));
        }

        [Test]
        public void CreateDBWithTaleAndSave()
        {
            var db = dbController.CreateDB("TestDB", "D:\\CSC\\4th\\IT\\Lab1");
            Table table = new Table("Users");
            table.AddColumn(new Column("Id", true, ColumnType.Int, true));
            table.AddColumn(new Column("Name", false, ColumnType.String, true));
            db.AddTable(table);
            dbController.SaveDbToDisk(db);
            Assert.IsTrue(File.Exists(Path.Combine(db.FolderPath, table.Name + ".tb")));
        }

        [Test]
        public void LoadDBFromDisk()
        {
            var db = dbController.CreateDB("TestDB", "D:\\CSC\\4th\\IT\\Lab1");
            dbController.SaveDbToDisk(db);
            var loadedDb = dbController.LoadDbFromDisk(db.FolderPath);
            Assert.AreEqual(db.Name, loadedDb.Name);
            Assert.AreEqual(db.RootDirectoryPath, loadedDb.RootDirectoryPath);
        }

        [Test]
        public void LoadDBWithTableFromDisk()
        {
            var db = dbController.CreateDB("TestDB", "D:\\CSC\\4th\\IT\\Lab1");
            Table table = new Table("Users");
            table.AddColumn(new Column("Id", true, ColumnType.Int, true));
            table.AddColumn(new Column("Name", false, ColumnType.String, true));
            db.AddTable(table);
            dbController.SaveDbToDisk(db);
            var loadedDb = dbController.LoadDbFromDisk(db.FolderPath);
            Assert.AreEqual(db.Name, loadedDb.Name);
            Assert.AreEqual(db.RootDirectoryPath, loadedDb.RootDirectoryPath);
            Assert.AreEqual(db.Tables.Count, loadedDb.Tables.Count);
            foreach(var originalTable in db.Tables)
            {
                var loadedTable = loadedDb.GetTable(originalTable.Name);
                Assert.IsNotNull(loadedTable);
                Assert.AreEqual(originalTable.Columns.Count, loadedTable.Columns.Count);
                foreach(var field in originalTable.Columns)
                {
                    var loadedField = loadedTable.GetColumn(field.Name);
                    Assert.IsNotNull(loadedField);
                    Assert.AreEqual(field.Id, loadedField.Id);
                    Assert.AreEqual(field.Name, loadedField.Name);
                    Assert.AreEqual(field.IsPrimaryKey, loadedField.IsPrimaryKey);
                    Assert.AreEqual(field.Type, loadedField.Type);
                    Assert.AreEqual(field.Nullable, loadedField.Nullable);
                }
            }
        }

        [Test]
        public void LoadDBWithTableFromDiskAndCheckData()
        {
            var db = dbController.CreateDB("TestDB", "D:\\CSC\\4th\\IT\\Lab1");
            Table table = new Table("Users");
            table.AddColumn(new Column("Id", true, ColumnType.Int, true));
            table.AddColumn(new Column("Name", false, ColumnType.String, true));
            db.AddTable(table);
            var data = new List<DataRow>             
            {
                new DataRow
                {
                    Values = new List<DataValue>
                    {
                        new DataValue { Column = table.GetColumn("Id"), Value = 1 },
                        new DataValue { Column = table.GetColumn("Name"), Value = "John" }
                    }
                },
                new DataRow
                {
                    Values = new List<DataValue>
                    {
                        new DataValue { Column = table.GetColumn("Id"), Value = 2 },
                        new DataValue { Column = table.GetColumn("Name"), Value = "Jane" }
                    }
                }
            };

            table.AddData(data);
            dbController.SaveDbToDisk(db);
            var reloadedDb = dbController.LoadDbFromDisk(db.FolderPath);
            var reloadedTable = db.GetTable(table.Name);
            Assert.IsNotNull(reloadedTable);
            var reloadedData = reloadedTable.GetData();
            Assert.AreEqual(data.Count, reloadedData.Count);
            foreach(var row in data)
            {
                var reloadedRow = reloadedData.FirstOrDefault(r => r.Values.All(v => row.Values.Any(rv => rv.Column.Name == v.Column.Name && rv.Value.Equals(v.Value))));
                Assert.IsNotNull(reloadedRow);
            }
        }
    }
}