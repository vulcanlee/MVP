using CommunityToolkit.Mvvm.ComponentModel;
using NckuhRepair.Models;
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
        [ObservableProperty]
        ObservableCollection<FormRecordItem> formRecordItems =
            new ObservableCollection<FormRecordItem>();

        public FormRecordViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
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
            FormRecordItems.Clear();
            FormRecordItems.Add(new FormRecordItem()
            {
                Title = "軟體叫修",
                CreateAt = DateTime.Now.AddDays(-1),
                Form = new FormIOModel()
                {
                }
            });
            FormRecordItems.Add(new FormRecordItem()
            {
                Title = "軟體叫修",
                CreateAt = DateTime.Now.AddDays(-2),
                Form = new FormIOModel()
                {
                }
            });
            FormRecordItems.Add(new FormRecordItem()
            {
                Title = "硬體叫修",
                CreateAt = DateTime.Now.AddDays(-5),
                Form = new FormIOModel()
                {
                }
            });
            FormRecordItems.Add(new FormRecordItem()
            {
                Title = "PACS叫修",
                CreateAt = DateTime.Now.AddDays(-10),
                Form = new FormIOModel()
                {
                }
            });
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
