using BaoYaoYao.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoYaoYao.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject, INavigatedAware
    {
        private readonly INavigationService navigationService;

        public LoginPageViewModel(INavigationService navigationService)
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
        public async Task Register()
        {
            NavigationParameters para = new NavigationParameters();
            para.Add(KnownNavigationParameters.Animated, false);

            await navigationService.NavigateAsync("/NaviPage/RegistrationPage", para);

            //await navigationService.CreateBuilder()
            //    .UseAbsoluteNavigation()
            //    .AddSegment<NaviPageViewModel>()
            //    .AddSegment<RegistrationPageViewModel>()
            //    .NavigateAsync();
        }
        [RelayCommand]
        public async Task Login()
        {
            //var result = await SendMessage.MessageHttpClient("Subject", "123abcDEF測試", "0939133434");
            //return;
            await navigationService.NavigateAsync("/NaviPage/ConnectPharmacyPage");
        }
        #endregion
    }
}
