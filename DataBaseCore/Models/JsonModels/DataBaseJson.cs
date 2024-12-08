using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseCore.Models.JsonModels
{
    public class DataBaseJson
    {
        public string Name { get; set; }
        public string RootDirectoryPath { get; set; }
        public List<string> TableNames { get; set; }

        [JsonIgnore] public string FolderPath => Path.Combine(RootDirectoryPath, Name); 
        [JsonIgnore] public List<TableJson> Tables { get; set; }

        public DataBaseJson()
        {
            
        }

        public DataBaseJson(DataBase db)
        {
            this.Name = db.Name;
            this.RootDirectoryPath = db.RootDirectoryPath;
            this.Tables = new List<TableJson>();
            this.TableNames = db.Tables.Select(t => t.Name).ToList();
            foreach(var table in db.Tables)
            {
                this.Tables.Add(new TableJson(table));
            }
        }
    }
}
