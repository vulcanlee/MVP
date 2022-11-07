using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NckuhRepair.Helpers;
using NckuhRepair.Models;
using NckuhRepair.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NckuhRepair.ViewModels
{
    public partial class FormRecordViewModel : ObservableObject, INavigatedAware, IActiveAware
    {
        private readonly INavigationService navigationService;
        private readonly FormItemService formItemService;
        private readonly MagicHelper magicHelper;
        [ObservableProperty]
        ObservableCollection<FormRecordItem> formRecordItems =
            new ObservableCollection<FormRecordItem>();

        public FormRecordViewModel(INavigationService navigationService,
            FormItemService formItemService, MagicHelper magicHelper)
        {
            this.navigationService = navigationService;
            this.formItemService = formItemService;
            this.magicHelper = magicHelper;
        }

        [ObservableProperty]
        bool isActive;

        //public bool IsActive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler IsActiveChanged;

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        private async Task RefreshAsync()
        {
            await Task.Yield();
            await formItemService.ReadFromFileAsync();
            FormRecordItems.Clear();
            foreach (var item in formItemService.Items)
            {
                FormRecordItems.Add(new FormRecordItem()
                {
                    Title = item.title,
                    CreateAt = item.CreateAt,
                    Form = item,
                });
            }
        }

        #region CommunityToolkit 的 ObservableProperty Running code upon changes
        async partial void OnIsActiveChanged(bool value)
        {
            if (value)
            {
                await RefreshAsync();
            }   
        }
        
        protected virtual void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region 綁定頁面上的命令
        [RelayCommand]
        public async Task TapFormRecordItem(FormRecordItem formRecordItem)
        {
            await Task.Yield();

            #region 開啟頁面來顯示這張表單
            string formJSON = JsonConvert.SerializeObject(formRecordItem.Form);

            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(magicHelper.FormIOModelNavigationParameterName, formRecordItem.Form);
            parameters.Add(magicHelper.JSONNavigationParameterName, formJSON);
            parameters.Add(magicHelper.FormEditModeNavigationParameterName, false);

            await navigationService.CreateBuilder()
                .WithParameters(parameters)
                .AddSegment<FormIOPageViewModel>()
                .NavigateAsync();

            #endregion
        }
        #endregion
    }
}
