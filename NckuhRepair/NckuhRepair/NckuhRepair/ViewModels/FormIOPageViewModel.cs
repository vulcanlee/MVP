using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NckuhRepair.Helpers;
using NckuhRepair.Models;
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

    public FormIOPageViewModel(INavigationService navigationService,
        IPageDialogService dialogService, FormIOVerifyHelper formIOVerifyHelper)
    {
        this.navigationService = navigationService;
        this.dialogService = dialogService;
        this.formIOVerifyHelper = formIOVerifyHelper;
    }

    public FormIOModel FormIOModel { get; set; } = null;
    public string Json { get; set; }
    public Action BuildFormObject { get; set; }
    public bool ReadSuccessful { get; set; } = false;

    [RelayCommand]
    async void Save()
    {
        var result = formIOVerifyHelper.CheckRequired(FormIOModel);
        if(string.IsNullOrEmpty(result)==false)
        {
            await dialogService.DisplayAlertAsync("錯誤",
                $"{result}", "確定");
            return;
        }
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
