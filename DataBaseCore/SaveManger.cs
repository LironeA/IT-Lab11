using DataBaseCore.Models;
using DataBaseCore.Models.JsonModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataBaseCore
{
    public static class SaveManger
    {

        public static void Save(DataBase db)
        {
            var jsonModel = new DataBaseJson(db);

            if(!Directory.Exists(jsonModel.FolderPath))
            {
                Directory.CreateDirectory(jsonModel.FolderPath);
            }

            var json = JsonConvert.SerializeObject(jsonModel);
            File.WriteAllText(Path.Combine(jsonModel.FolderPath, jsonModel.Name + ".db"), json);

            foreach(var table in jsonModel.Tables)
            {
                var tableJson = JsonConvert.SerializeObject(table);
                File.WriteAllText(Path.Combine(db.FolderPath, table.Name + ".tb"), tableJson);
            }

        }


        public static DataBase Load(string path)
        {
            if(!Directory.Exists(path))
            {
                return null;
            }
            DataBase db = null;
            Directory.EnumerateFiles(path, "*.db").ToList().ForEach(file =>
            {
                var json = File.ReadAllText(file);
                var dbJson = JsonConvert.DeserializeObject<DataBaseJson>(json);
                dbJson.Tables = new List<TableJson>();
                dbJson.TableNames.ForEach(tableName =>
                {
                    var tableJson = File.ReadAllText(Path.Combine(dbJson.FolderPath, tableName + ".tb"));
                    var tableModel = JsonConvert.DeserializeObject<TableJson>(tableJson);
                    dbJson.Tables.Add(tableModel);
                });
                db = new DataBase(dbJson);
            });
            return db;

        }

        internal static void Delete(string folderPath)
        {
            if(Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
        }

        internal static void DeleteTable(string folderPath, string name)
        {
            if(File.Exists(Path.Combine(folderPath, name + ".tb")))
            {
                //File.Delete(Path.Combine(folderPath, name + ".tb"));
            }
        }
    }
}
