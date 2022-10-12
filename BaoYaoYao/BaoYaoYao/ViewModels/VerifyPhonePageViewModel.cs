using BaoYaoYao.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BaoYaoYao.ViewModels
{
    public partial class VerifyPhonePageViewModel : ObservableObject, INavigatedAware
    {
        private readonly INavigationService navigationService;
        [ObservableProperty]
        string key1 = "";
        [ObservableProperty]
        string key2 = "";
        [ObservableProperty]
        string key3 = "";
        [ObservableProperty]
        string key4 = "";
        [ObservableProperty]
        ObservableCollection<PressNumberKey> pressNumberKeys = new ObservableCollection<PressNumberKey>();
        public VerifyPhonePageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            #region 初始化按鍵
            PressNumberKeys.Clear();
            pressNumberKeys.Add(new PressNumberKey() { KeyName = "1" });
            pressNumberKeys.Add(new PressNumberKey() { KeyName = "2" });
            pressNumberKeys.Add(new PressNumberKey() { KeyName = "3" });
            pressNumberKeys.Add(new PressNumberKey() { KeyName = "4" });
            pressNumberKeys.Add(new PressNumberKey() { KeyName = "5" });
            pressNumberKeys.Add(new PressNumberKey() { KeyName = "6" });
            pressNumberKeys.Add(new PressNumberKey() { KeyName = "7" });
            pressNumberKeys.Add(new PressNumberKey() { KeyName = "8" });
            pressNumberKeys.Add(new PressNumberKey() { KeyName = "9" });
            pressNumberKeys.Add(new PressNumberKey() { KeyName = "" });
            pressNumberKeys.Add(new PressNumberKey() { KeyName = "0" });
            pressNumberKeys.Add(new PressNumberKey() { KeyName = "C" });
            #endregion
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
        [RelayCommand]
        public void VerifyCodePress(string key)
        {
            if (string.IsNullOrEmpty(key)) return;
            if (key == "C")
            {
                Key1 = "";
                Key2 = "";
                Key3 = "";
                Key4 = "";
                return;
            }

            UpdateKey(key);
        }

        void UpdateKey(string key)
        {
            if (Key1 == "") { Key1 = key; return; }
            if (Key2 == "") { Key2 = key; return; }
            if (Key3 == "") { Key3 = key; return; }
            if (Key4 == "") { Key4 = key; return; }
        }
    }
}
