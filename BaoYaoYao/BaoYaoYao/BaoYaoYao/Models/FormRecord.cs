using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoYaoYao.Models
{
    public partial class FormRecord :ObservableObject
    {
        [ObservableProperty]
        string name = "";
        [ObservableProperty]
        string identityCard = "";
        [ObservableProperty]
        bool gender = false;
        [ObservableProperty]
        string phone = "";
        [ObservableProperty]
        DateTime birthday;
    }
}
