using CommunityToolkit.Mvvm.ComponentModel;
using JsonForm.Models;
using Newtonsoft.Json;

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
            var mobileForm = JsonConvert.DeserializeObject<MobileForm>(result);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }
}
