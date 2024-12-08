using DataBaseCore.Models.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseCore.Models
{
    public class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Column> Columns { get; set; }
        public List<DataRow> Data { get; set; }

        public Table(string name)
        {
            this.Name = name;
            this.Columns = new List<Column>();
            this.Data = new List<DataRow>();
        }

        public Table(TableJson tableJson)
        {
            this.Name = tableJson.Name;
            this.Columns = new List<Column>();
            foreach(var fieldJson in tableJson.Fields)
            {
                this.Columns.Add(new Column(fieldJson));
            }

            this.Data = new List<DataRow>();
            foreach(var rowJson in tableJson.Data)
            {
                var dataRow = new DataRow();
                dataRow.Values = new List<DataValue>();
                foreach(var valueJson in rowJson.Values)
                {
                    var column = this.Columns.FirstOrDefault(f => f.Id == valueJson.FieldId);
                    dataRow.Values.Add(new DataValue { Column = column, Value = column.Parse(valueJson.Value) });
                }
                this.Data.Add(dataRow);
            }
        }

        public void AddColumn(Column column)
        {
            Columns.Add(column);
            foreach(var row in Data)
            {
                row.Values.Add(new DataValue { Column = column, Value = column.DefaultValue });
            }
        }

        public Column GetColumn(string name)
        {
            return Columns.FirstOrDefault(f => f.Name == name);
        }

        public void AddData(List<DataRow> data)
        {
            Data.AddRange(data);
        }

        public void AddData(DataRow data)
        {
            Data.Add(data);
        }

        public List<DataRow> GetData()
        {
            return Data;
        }
    }
}
