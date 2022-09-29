using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoYaoYao.ViewModels
{
    public partial class BarCodeScanPageViewModel : ObservableObject, INavigatedAware
    {
        private readonly INavigationService navigationService;

        [ObservableProperty]
        string qRCodeScanResult = "";

        public BarCodeScanPageViewModel(INavigationService navigationService)
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
        public void GoBack()
        {
            GoBackApplyPage();
        }

        public void GoBackApplyPage()
        {
            //await navigationService.NavigateAsync("ApplyPage");
            //await navigationService.NavigateAsync("/NaviPage/ConnectPharmacyPage");

            navigationService.CreateBuilder()
                .UseAbsoluteNavigation()
                .AddSegment("NaviPage")
                .AddSegment("ConnectPharmacyPage")
                .AddSegment("ApplyPage")
                .Navigate();

        }
    }
}
