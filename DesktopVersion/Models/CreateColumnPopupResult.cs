using DataBaseCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.Models
{
    public class CreateColumnPopupResult
    {
        public string Name { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool Nullable { get; set; }
        public string DefaultValue { get; set; }
        public ColumnType Type { get; set; }

    }
}
