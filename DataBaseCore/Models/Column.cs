using DataBaseCore.Models.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseCore.Models
{
    public class Column
    {
        public int Id { get; set; }
        public bool IsPrimaryKey { get; set; }
        public string Name { get; set; }
        public bool Nullable { get; set; }
        public string DefaultValue { get; set; }
        public ColumnType Type { get; set; }

        public Column()
        {

        }

        public Column( string name, bool isPrimaryKey, ColumnType type, bool nullable, string defaultValue = null)
        {
            this.Name = name;
            this.IsPrimaryKey = isPrimaryKey;
            this.Type = type;
            this.Nullable = nullable;
            this.DefaultValue = defaultValue;
            
        }

        public Column(ColumnJson tableJson)
        {
            this.Id = tableJson.Id;
            this.Name = tableJson.Name;
            this.Type = tableJson.Type;
            this.Nullable = tableJson.Nullable;
            this.DefaultValue = tableJson.DefaultValue;
            this.IsPrimaryKey = tableJson.IsPrimaryKey;
        }

        public string ToString(object value)
        {
            try
            {
                switch(Type)
                {
                    case ColumnType.Int:
                    case ColumnType.Float:
                    case ColumnType.Char:
                    case ColumnType.String:
                    case ColumnType.Bool:
                    return value.ToString();
                }
            }
            catch
            {

            }
            return string.Empty;
        }

        public object Parse(string value)
        {
            try
            {
                switch(Type)
                {
                    case ColumnType.Int:
                    int intValue = 0;
                    var intResult = int.TryParse(value, out intValue);
                    if(intResult)
                    {
                        return intValue;
                    }
                    break;

                    case ColumnType.Float:
                    float floatValue = 0f;
                    var floatResult = float.TryParse(value, out floatValue);
                    if(floatResult)
                    {
                        return floatResult;
                    }
                    break;

                    case ColumnType.Char:
                    var c = char.Parse(value);
                    return c;
                    break;

                    case ColumnType.String:
                    return value;
                    break;

                    case ColumnType.Bool:
                    bool boolValue = false;
                    var boolResult = bool.TryParse(value, out boolValue);
                    if(boolResult)
                    {
                        return boolValue;
                    }
                    break;
                }
            }
            catch
            {

            }
            return null;
        }
        
    }

    public enum ColumnType
    {
        Int,
        Float,
        String,
        Bool,
        Char,
        Picture,
        RealInvl
    }
}
