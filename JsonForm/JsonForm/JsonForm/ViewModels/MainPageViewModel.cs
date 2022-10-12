using CommunityToolkit.Mvvm.ComponentModel;
using JsonForm.Models;
using JsonForm.Views;
using Newtonsoft.Json;
using Prism.Navigation;

namespace JsonForm.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    private readonly INavigationService navigationService;
    public MainPageViewModel(INavigationService navigationService)
    {
        this.navigationService = navigationService;
    }

    [CommunityToolkit.Mvvm.Input.RelayCommand]
    async Task ShowJsonFormAsync(string jsonFilename)
    {
        try
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync(jsonFilename);
            using var reader = new StreamReader(stream);
            var result = await reader.ReadToEndAsync();

            if (jsonFilename == "WorkOrderPanel.json")
            {
                #region 採用 form.io 產生的 JSON
                var mobileForm = JsonConvert.DeserializeObject<FormIOModel>(result);

                NavigationParameters parameters = new NavigationParameters();
                parameters.Add("FormIOModel", mobileForm);
                parameters.Add("JSON", result);

                await navigationService.CreateBuilder()
                    .WithParameters(parameters)
                    .AddSegment<FormIOPageViewModel>()
                    .NavigateAsync();
                #endregion
            }
            else
            {
                #region 舊版本 第一代
                var mobileForm = JsonConvert.DeserializeObject<MobileForm>(result);

                NavigationParameters parameters = new NavigationParameters();
                parameters.Add("MobileForm", mobileForm);
                parameters.Add("JSON", result);

                await navigationService.CreateBuilder()
                    .WithParameters(parameters)
                    .AddSegment<FormPageViewModel>()
                    .NavigateAsync();
                #endregion
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }
}
