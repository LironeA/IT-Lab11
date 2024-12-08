namespace DataBaseCore.Models.JsonModels
{
    public class DataRowJson
    {
        public List<DataValueJson> Values { get; set; }

        public DataRowJson()
        {

        }

        public DataRowJson(DataRow dataRow)
        {
            this.Values = new List<DataValueJson>();
            foreach(var value in dataRow.Values)
            {
                this.Values.Add(new DataValueJson()
                {
                    FieldId = value.Column.Id,
                    Value = value.Column.ToString(value.Value)
                });
            }
        }
    }

    public class DataValueJson
    {
        public int FieldId { get; set; }
        public string Value { get; set; }

    }
}