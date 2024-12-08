namespace DataBaseCore.Models.JsonModels
{
    public class ColumnJson
    {
        public int Id { get; set; }
        public bool IsPrimaryKey { get; set; }
        public string Name { get; set; }
        public bool Nullable { get; set; }
        public string DefaultValue { get; set; }
        public ColumnType Type { get; set; }

        public ColumnJson()
        {
            
        }

        public ColumnJson(Column f)
        {
            this.Id = f.Id;
            this.Name = f.Name;
            this.Type = f.Type;
            this.Nullable = f.Nullable;
            this.DefaultValue = f.DefaultValue;
            this.IsPrimaryKey = f.IsPrimaryKey;
        }

    }
}