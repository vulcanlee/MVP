using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JsonForm.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonForm.ViewModels
{
    public partial class FormIOPageViewModel : ObservableObject, INavigatedAware
    {
        private readonly INavigationService navigationService;
        private readonly IPageDialogService dialogService;

        public FormIOPageViewModel(INavigationService navigationService,
            IPageDialogService dialogService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
        }

        public FormIOModel FormIOModel { get; set; } = null;
        public string Json { get; set; }
        public Action BuildFormObject { get; set; }
        public bool ReadSuccessful { get; set; } = false;

        [RelayCommand]
        void Save()
        {
            var mobileFormJson = JsonConvert.SerializeObject(FormIOModel);
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
            FormIOModel = parameters.GetValue<FormIOModel>("FormIOModel");
            Json = parameters.GetValue<string>("JSON");
            ReadSuccessful = true;
        }
    }
}
