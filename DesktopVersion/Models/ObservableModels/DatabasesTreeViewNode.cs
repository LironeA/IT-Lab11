using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.Models.ObservableModels
{
    public class DatabasesTreeViewNode : ObservableObject
    {
        public string Name { get; set; }
        public int parentId { get; set; }
        public NodeType NodeType { get; set; }
        public object NodeObject { get; set; }
        public ObservableCollection<DatabasesTreeViewNode> Children { get; set; }

        public DatabasesTreeViewNode()
        {
            Children = new ObservableCollection<DatabasesTreeViewNode>();
        }

    }

    public enum NodeType
    {
        DataBaseNode,
        TableNode,
        ColumnNode,
    }

}
