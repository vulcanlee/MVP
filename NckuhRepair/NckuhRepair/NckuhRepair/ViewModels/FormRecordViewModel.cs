using CommunityToolkit.Mvvm.ComponentModel;
using NckuhRepair.Models;
using NckuhRepair.Services;
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
        [ObservableProperty]
        ObservableCollection<FormRecordItem> formRecordItems =
            new ObservableCollection<FormRecordItem>();

        public FormRecordViewModel(INavigationService navigationService,
            FormItemService formItemService)
        {
            this.navigationService = navigationService;
            this.formItemService = formItemService;
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
        #endregion
        
        protected virtual void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
