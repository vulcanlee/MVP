﻿using CommunityToolkit.Mvvm.ComponentModel;
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
            await navigationService.CreateBuilder()
                .UseAbsoluteNavigation(true)
                .AddSegment<RegistrationPageViewModel>()
                .NavigateAsync();
        }
        #endregion
    }
}