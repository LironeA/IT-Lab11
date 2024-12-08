using DataBaseCore.Models.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseCore.Models
{
    public class DataBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RootDirectoryPath { get; set; }
        public string FolderPath => Path.Combine(RootDirectoryPath, Name);

        public List<Table> Tables { get; set; }
        public DataBase()
        {
            Tables = new List<Table>();
        }

        public DataBase(string name, string path) : base()
        {
            this.Name = name;
            this.RootDirectoryPath = path;
        }

        public DataBase(string path) : base()
        {
            this.RootDirectoryPath = path;
        }

        public DataBase(DataBaseJson dataBaseJson)
        {
            this.Name = dataBaseJson.Name;
            this.RootDirectoryPath = dataBaseJson.RootDirectoryPath;
            this.Tables = new List<Table>();
            foreach(var tableJson in dataBaseJson.Tables)
            {
                this.Tables.Add(new Table(tableJson));
            
            }
        }

        public Table GetTable(string name)
        {
            return Tables.FirstOrDefault(t => t.Name == name);
        }

        public void AddTable(Table table)
        {
            Tables.Add(table);
        }
    }
}
