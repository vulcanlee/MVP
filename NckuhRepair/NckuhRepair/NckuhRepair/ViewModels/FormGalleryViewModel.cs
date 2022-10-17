using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NckuhRepair.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NckuhRepair.ViewModels
{
    public partial class FormGalleryViewModel : ObservableObject, INavigatedAware
    {
        private readonly INavigationService navigationService;
        [ObservableProperty]
        ObservableCollection<FormItem> formItems = new ObservableCollection<FormItem>();
        public FormGalleryViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            await Refresh();
        }

        #region ViewModel 命令
        [RelayCommand]
        public async Task TapFormItem(FormItem formItem)
        {
            await Task.Yield();
        }
        #endregion

        public async Task Refresh()
        {
            await Task.Yield();
            FormItems.Clear();
            FormItems.Add(new FormItem()
            {
                ChineseName = "軟體叫修",
                EnglishName = "Software called repair",
                Color = Color.FromArgb("#FFADD8E2"),
            });
            FormItems.Add(new FormItem()
            {
                ChineseName = "硬體叫修",
                EnglishName = "Hareware repair",
                Color = Color.FromArgb("#FFEEA03B"),
            });
            FormItems.Add(new FormItem()
            {
                ChineseName = "PACS叫修",
                EnglishName = "PACS repair",
                Color = Color.FromArgb("#FF87C0A6"),
            });
            FormItems.Add(new FormItem()
            {
                ChineseName = "報告系統叫修",
                EnglishName = "Reporting system called repair",
                Color = Color.FromArgb("#FFDA5D3D"),
            });
        }
    }
}
