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
        async Task GoApplyForm(string parameter)
        {
            if (parameter == "軟體")
            {
                await navigationService.NavigateAsync("軟體Page");
            }
            else if (parameter == "硬體")
            {
                await navigationService.NavigateAsync("硬體Page");
            }
            else if (parameter == "PACS")
            {
                await navigationService.NavigateAsync("PACSPage");
            }
            else if (parameter == "報告系統")
            {
                await navigationService.NavigateAsync("報告系統Page");
            }
            else
            {

            }
        }

        [RelayCommand]
        async Task GoFormHistory()
        {
            await navigationService.NavigateAsync("ApplyHistoryPage");
        }
    }
}
