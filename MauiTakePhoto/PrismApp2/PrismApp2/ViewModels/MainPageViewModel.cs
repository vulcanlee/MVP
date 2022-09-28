using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PrismApp2.ViewModels;

public partial class MainPageViewModel : ObservableObject,INavigatedAware
{
    private ISemanticScreenReader _screenReader { get; }
    int count;

    public MainPageViewModel(ISemanticScreenReader screenReader,
        INavigationService navigationService)
    {
        _screenReader = screenReader;
        this.navigationService = navigationService;
    }

    [ObservableProperty]
    string title = "Main Page";
    [ObservableProperty]
    string text = "Click me";
    private readonly INavigationService navigationService;


    [RelayCommand]
    private async void Count()
    {
        count++;
        if (count == 1)
            Text = "Clicked 1 time";
        else if (count > 1)
            Text = $"Clicked {count} times";

        _screenReader.Announce(Text);

        navigationService.NavigateAsync("NextPage");

        return;

        if (MediaPicker.Default.IsCaptureSupported)
        {
            try
            {

                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // save the file into local storage
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }

    public void OnNavigatedFrom(INavigationParameters parameters)
    {
    }

    public void OnNavigatedTo(INavigationParameters parameters)
    {
    }
}
