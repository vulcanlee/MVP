using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JsonForm.Models;
using Newtonsoft.Json;
using Prism.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonForm.ViewModels
{
    public partial class FormPageViewModel:ObservableObject, INavigatedAware
    {
        private readonly INavigationService navigationService;
        private readonly IPageDialogService dialogService;

        public FormPageViewModel(INavigationService navigationService,
            IPageDialogService dialogService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
        }

        public MobileForm MobileForm { get; set; } = null;
        public string Json { get; set; }
        public Action BuildFormObject { get; set; }
        public bool ReadSuccessful { get; set; } = false;

        [RelayCommand]
        void Save()
        {
            var mobileFormJson = JsonConvert.SerializeObject(MobileForm);
            dialogService.DisplayAlertAsync("通知", $"{mobileFormJson}", "OK");
        }

        [RelayCommand]
        void Upload()
        {

        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            ReadSuccessful = false;
            MobileForm = parameters.GetValue<MobileForm>("MobileForm");
            Json = parameters.GetValue<string>("JSON");
            ReadSuccessful = true;
        }
    }
}
