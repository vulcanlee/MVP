using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace wpfMVVM
{

    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            MyDataItems = new ObservableCollection<MyDataItem>()
            {
                new MyDataItem()
                {
                    Name = "sjflskjfsljsjf435324234324lskjfsljsjflskjfsljsjflskjfsljsjflskjfslj",
                    IsSpecial =  System.Windows.Visibility.Collapsed,
                },
                new MyDataItem()
                {
                    Name = "sjflskjfslj",
                    IsSpecial = System.Windows.Visibility.Collapsed,
                },
                new MyDataItem()
                {
                    Name = "sjflskjfsljsjflskjfsljsjflskjfsljsjflskjfsljsjflskjfslj",
                    IsSpecial = System.Windows.Visibility.Visible,
                },
                new MyDataItem()
                {
                    Name = "sjflskjfsljsjflskjfsljsjflskjfsljsjflskjfsljsjflskjfslj",
                    IsSpecial = System.Windows.Visibility.Collapsed,
                },
            };
        }

        public ObservableCollection<MyDataItem> MyDataItems { get; set; } = new ObservableCollection<MyDataItem>();

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
