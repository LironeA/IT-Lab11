using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseCore.Models
{
    public class DataRow
    {
        public int Id { get; set; }
        public List<DataValue> Values { get; set; }
    }

    public class DataValue
    {
        public int Id { get; set; }
        public Column Column { get; set; }
        public object Value { get; set; }
    }
}
