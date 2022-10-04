using BaoYaoYao.Helpers;
using BaoYaoYao.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Common;
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
        private readonly MagicObjectHelper magicObjectHelper;
        [ObservableProperty]
        string qRCodeScanResult = "";
        [ObservableProperty]
        FormRecord formRecord = new FormRecord();

        public BarCodeScanPageViewModel(INavigationService navigationService,
            MagicObjectHelper magicObjectHelper)
        {
            this.navigationService = navigationService;
            this.magicObjectHelper = magicObjectHelper;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if(parameters.ContainsKey(magicObjectHelper.FormRecordName))
            {
                formRecord = parameters.GetValue<FormRecord>(magicObjectHelper.FormRecordName);
            }
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
                .AddParameter(magicObjectHelper.FormRecordName, FormRecord)
                .AddSegment("ApplyPage")
                .Navigate();

        }
    }
}
