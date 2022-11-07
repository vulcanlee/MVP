using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NckuhRepair.Helpers;
using NckuhRepair.Models;
using NckuhRepair.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NckuhRepair.ViewModels;

public partial class FormIOPageViewModel : ObservableObject, INavigatedAware
{
    private readonly INavigationService navigationService;
    private readonly IPageDialogService dialogService;
    private readonly FormIOVerifyHelper formIOVerifyHelper;
    private readonly FormItemService formItemService;

    public FormIOPageViewModel(INavigationService navigationService,
        IPageDialogService dialogService, FormIOVerifyHelper formIOVerifyHelper,
        FormItemService formItemService)
    {
        this.navigationService = navigationService;
        this.dialogService = dialogService;
        this.formIOVerifyHelper = formIOVerifyHelper;
        this.formItemService = formItemService;
    }

    public FormIOModel FormIOModel { get; set; } = null;
    public string Json { get; set; }
    public Action BuildFormObject { get; set; }
    public bool ReadSuccessful { get; set; } = false;

    [RelayCommand]
    async Task Save()
    {
        var result = formIOVerifyHelper.CheckRequired(FormIOModel);
        if (string.IsNullOrEmpty(result) == false)
        {
            await dialogService.DisplayAlertAsync("錯誤",
                $"{result}", "確定");
            return;
        }

        if (formItemService.Items.Count == 0) await formItemService.ReadFromFileAsync();
        formItemService.Items.Add(FormIOModel);
        await dialogService.DisplayAlertAsync("通知", $"已經儲存", "OK");
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
