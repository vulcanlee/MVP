﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoYaoYao.ViewModels
{
    public partial class SplashPageViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService navigationService;

        public SplashPageViewModel(INavigationService navigationService )
        {
            this.navigationService = navigationService;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            await Task.Delay(3000);
            await navigationService.CreateBuilder()
                .AddSegment<LoginPageViewModel>()
                .NavigateAsync();
        }
    }
}
