using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BaoYaoYao.ViewModels
{
    public partial class VerifyPhonePageViewModel : ObservableObject, INavigatedAware
    {
        private readonly INavigationService navigationService;

        public VerifyPhonePageViewModel(INavigationService navigationService)
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
        public async Task Login()
        {
            await navigationService.NavigateAsync("/NaviPage/ConnectPharmacyPage");
        }
    }
}
