using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace wpfMVVM
{
    public class MyDataItem
    {
        public string Name { get; set; } = "";
        public Visibility IsSpecial { get; set; } = Visibility.Collapsed;
    }
}
