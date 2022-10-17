using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NckuhRepair.Models
{
    public partial class FormItem : ObservableObject
    {
        [ObservableProperty]
        string chineseName = "";
        [ObservableProperty]
        string englishName = "";
        [ObservableProperty]
        Color color = Colors.Transparent;
    }
}
