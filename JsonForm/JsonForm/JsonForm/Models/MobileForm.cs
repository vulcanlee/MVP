using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonForm.Models
{

    public class MobileForm
    {
        public Page Page { get; set; } = new Page();
    }

    public class Page
    {
        public List<Row> Rows { get; set; } = new List<Row>();
    }

    public class Row
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Required { get; set; }
        public string Width { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string ColumnsWidth { get; set; }
        public List<Column> Columns { get; set; }
        public Dictionary<string, string> Options { get; set; }
            = new Dictionary<string, string>();
    }
    public class Column
    {
        public Viewitem[] ViewItems { get; set; }
    }

    public class Viewitem
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public Dictionary<string, string> Options { get; set; }
            = new Dictionary<string, string>();
        public string Width { get; set; }
        public string Title { get; set; }
    }

    public class Options
    {
        public string a { get; set; }
        public string b { get; set; }
        public string c { get; set; }
        public string _1 { get; set; }
        public string _2 { get; set; }
        public string _3 { get; set; }
    }
}
