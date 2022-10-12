using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoYaoYao.Models
{
    public partial class PressNumberKey :ObservableObject
    {
        [ObservableProperty]
        string keyName = "";
    }
}
