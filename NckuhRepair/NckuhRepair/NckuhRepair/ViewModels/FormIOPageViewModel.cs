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
    private readonly MagicHelper magicHelper;

    public FormIOPageViewModel(INavigationService navigationService,
        IPageDialogService dialogService, FormIOVerifyHelper formIOVerifyHelper,
        FormItemService formItemService, MagicHelper magicHelper)
    {
        this.navigationService = navigationService;
        this.dialogService = dialogService;
        this.formIOVerifyHelper = formIOVerifyHelper;
        this.formItemService = formItemService;
        this.magicHelper = magicHelper;
    }

    public FormIOModel FormIOModel { get; set; } = null;
    public string Json { get; set; }
    public bool FormEditMode { get; set; } = true;
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

        if (formItemService.Items == null || formItemService.Items.Count == 0) 
            await formItemService.ReadFromFileAsync();
        FormIOModel.CreateAt = DateTime.Now;
        formItemService.Items.Add(FormIOModel);
        await formItemService.WriteToFileAsync();
        
        await dialogService.DisplayAlertAsync("通知", $"已經儲存", "OK");

        await navigationService.GoBackAsync();
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
        FormIOModel = parameters.GetValue<FormIOModel>(magicHelper.FormIOModelNavigationParameterName);
        Json = parameters.GetValue<string>(magicHelper.JSONNavigationParameterName);
        FormEditMode = parameters.GetValue<bool>(magicHelper.FormEditModeNavigationParameterName);
        ReadSuccessful = true;
    }
}
