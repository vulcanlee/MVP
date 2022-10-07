using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace ViewModels
{

    public partial class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel()
        {
            MyDataItems = new List<MyDataItem>()
            {
                new MyDataItem()
                {
                    Name = "sjflskjfslj",
                    IsSpecial = false,
                },
                new MyDataItem()
                {
                    Name = "sjflskjfsljsjflskjfsljsjflskjfsljsjflskjfsljsjflskjfslj",
                    IsSpecial = false,
                },
                new MyDataItem()
                {
                    Name = "sjflskjfsljsjflskjfsljsjflskjfsljsjflskjfsljsjflskjfslj",
                    IsSpecial = true,
                },
            };
        }

        [ObservableProperty]
        List<MyDataItem> myDataItems;


    }
}
