using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonForm.Models
{

    public class MobileForm
    {
        public Page Page { get; set; }
    }

    public class Page
    {
        public Rows[] rows { get; set; }
    }

    public class Rows
    {
        public Row[] row { get; set; }
    }

    public class Row
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Required { get; set; }
        public string Width { get; set; }
        public string name { get; set; }
        public string Text { get; set; }
    }
}
