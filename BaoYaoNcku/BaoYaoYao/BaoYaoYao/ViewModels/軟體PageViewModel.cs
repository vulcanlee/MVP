using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoYaoYao.ViewModels
{
    public partial class 軟體PageViewModel : ObservableObject, INavigatedAware
    {
        private readonly INavigationService navigationService;

        public 軟體PageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
