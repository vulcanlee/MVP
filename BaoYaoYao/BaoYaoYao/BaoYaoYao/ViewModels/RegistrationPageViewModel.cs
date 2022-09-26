using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoYaoYao.ViewModels
{
    public partial class RegistrationPageViewModel:ObservableObject,INavigatedAware
    {
        private readonly INavigationService navigationService;

        public RegistrationPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        #region 綁定命令使用
        [RelayCommand]
        public async Task SwitchLoginMode()
        {
            await navigationService.NavigateAsync("/LoginPage");
        }

        [RelayCommand]
        public async Task NextVerify()
        {
            await navigationService.NavigateAsync($"VerifyPhonePage");
        }
        #endregion

    }
}
