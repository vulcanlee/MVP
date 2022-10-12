using BaoYaoYao.Events;
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
    public partial class RegistrationPageViewModel : ObservableObject, INavigatedAware
    {
        private readonly INavigationService navigationService;
        private readonly IPageDialogService dialogService;
        private readonly DataCheckVerifyHelper dataCheckVerifyHelper;
        private readonly IEventAggregator eventAggregator;
        private readonly MagicObjectHelper magicObjectHelper;
        [ObservableProperty]
        string phoneNumber = "";
        public RegistrationPageViewModel(INavigationService navigationService,
            IPageDialogService dialogService, DataCheckVerifyHelper dataCheckVerifyHelper,
            IEventAggregator eventAggregator, MagicObjectHelper magicObjectHelper)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.dataCheckVerifyHelper = dataCheckVerifyHelper;
            this.eventAggregator = eventAggregator;
            this.magicObjectHelper = magicObjectHelper;

#if DEBUG
            PhoneNumber = "0987654321";
#endif
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
            var checkResult = dataCheckVerifyHelper.IsHandset(PhoneNumber);
            if (checkResult == false)
            {
                await dialogService.DisplayAlertAsync("警告", "所輸入的手機號碼不正確", "確定");
                return;
            }

            eventAggregator.GetEvent<ShowPopupEvent>().Publish(new ShowPopupPayload()
            {
                IsShow = true,
                Target = magicObjectHelper.PageRegistration,
                UpdateMessage = "請稍後，正在檢查你的手機號碼"
            });

            await Task.Delay(2000);

            eventAggregator.GetEvent<ShowPopupEvent>().Publish(new ShowPopupPayload()
            {
                IsShow = false,
                NeedClosePopup = false,
                UpdateMessage = "準備要完成"
            });

            await Task.Delay(1000);

            eventAggregator.GetEvent<ShowPopupEvent>().Publish(new ShowPopupPayload()
            {
                IsShow = false,
                NeedClosePopup = true,
                Target = "",
                UpdateMessage = ""
            });

            await navigationService.NavigateAsync($"VerifyPhonePage");
        }
        #endregion

    }
}
