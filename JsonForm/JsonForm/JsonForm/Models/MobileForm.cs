using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonForm.Models
{

    public class MobileForm
    {
        public Page Page { get; set; }=new Page();
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
    }
}
