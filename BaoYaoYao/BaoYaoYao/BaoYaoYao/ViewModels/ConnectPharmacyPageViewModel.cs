using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoYaoYao.ViewModels
{
    public partial class ConnectPharmacyPageViewModel : ObservableObject, INavigatedAware
    {
        private readonly INavigationService navigationService;

        public ConnectPharmacyPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        [RelayCommand]
        async Task GoApplyForm()
        {
            await navigationService.NavigateAsync("ApplyPage");
        }
    }
}
