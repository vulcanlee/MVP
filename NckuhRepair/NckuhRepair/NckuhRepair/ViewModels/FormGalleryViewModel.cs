using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NckuhRepair.Helpers;
using NckuhRepair.Models;
using Newtonsoft.Json;
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
        private readonly MagicHelper magicHelper;
        [ObservableProperty]
        ObservableCollection<FormItem> formItems = new ObservableCollection<FormItem>();
        public FormGalleryViewModel(INavigationService navigationService,
            MagicHelper magicHelper)
        {
            this.navigationService = navigationService;
            this.magicHelper = magicHelper;
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
            try
            {
                string jsonFilename = "";
                jsonFilename = formItem.ChineseName switch
                {
                    "軟體叫修" => "software.json",
                    "硬體叫修" => "hardware.json",
                    "PACS叫修" => "pacs.json",
                    "報告系統叫修" => "report.json",
                };

                using var stream = await FileSystem.OpenAppPackageFileAsync(jsonFilename);
                using var reader = new StreamReader(stream);
                var result = await reader.ReadToEndAsync();

                #region 採用 form.io 產生的 JSON
                var mobileForm = JsonConvert.DeserializeObject<FormIOModel>(result);

                NavigationParameters parameters = new NavigationParameters();
                parameters.Add(magicHelper.FormIOModelNavigationParameterName, mobileForm);
                parameters.Add(magicHelper.JSONNavigationParameterName, result);
                parameters.Add(magicHelper.FormIOModelNavigationParameterName, true);

                await navigationService.CreateBuilder()
                    .WithParameters(parameters)
                    .AddSegment<FormIOPageViewModel>()
                    .NavigateAsync();
                #endregion
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
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
