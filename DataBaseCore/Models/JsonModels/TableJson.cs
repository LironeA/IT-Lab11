namespace DataBaseCore.Models.JsonModels
{
    public class TableJson
    {
        public string Name;
        public List<ColumnJson> Fields;
        public List<DataRowJson> Data { get; set; }

        public TableJson()
        {
            
        }

        public TableJson(Table tb)
        {
            this.Name = tb.Name;
            this.Fields = new List<ColumnJson>();
            foreach(var field in tb.Columns)
            {
                this.Fields.Add(new ColumnJson(field));
            }
            this.Data = new List<DataRowJson>();
            foreach(var row in tb.Data)
            {
                this.Data.Add(new DataRowJson(row));
            }
        }
    }
}