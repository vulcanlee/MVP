using BaoYaoYao.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoYaoYao.ViewModels
{
    public partial class ApplyHistoryPageViewModel : ObservableObject, INavigatedAware
    {
        private readonly INavigationService navigationService;
        [ObservableProperty]
        ObservableCollection<FormRecord> formRecords = new ObservableCollection<FormRecord>();

        public ApplyHistoryPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Random random = new Random();

            DateTime start = new DateTime(1950, 1, 1);

            int range = (DateTime.Today - start).Days;
            var birthday = start.AddDays(random.Next(range));
            FormRecords.Add(new FormRecord()
            {
                Name = "王曉明",
                Birthday = birthday
            });
            birthday = start.AddDays(random.Next(range));
            FormRecords.Add(new FormRecord()
            {
                Name = "長清晨",
                Birthday = birthday
            });
            birthday = start.AddDays(random.Next(range));
            FormRecords.Add(new FormRecord()
            {
                Name = "吳渺渺",
                Birthday = birthday
            });
            birthday = start.AddDays(random.Next(range));
            FormRecords.Add(new FormRecord()
            {
                Name = "林木森",
                Birthday = birthday
            });
            birthday = start.AddDays(random.Next(range));
            FormRecords.Add(new FormRecord()
            {
                Name = "黃曉明",
                Birthday = birthday
            });
        }
    }
}
