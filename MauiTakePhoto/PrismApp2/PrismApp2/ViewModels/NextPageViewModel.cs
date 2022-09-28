using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PrismApp2.ViewModels;

public partial class NextPageViewModel : ObservableObject,INavigatedAware
{
    private ISemanticScreenReader _screenReader { get; }
    private int _count;

    public NextPageViewModel(ISemanticScreenReader screenReader)
    {
        _screenReader = screenReader;
    }

    [ObservableProperty]
    string title = "Main Page";
    [ObservableProperty]
    string text = "Click me";
    private readonly INavigationService navigationService;


    [RelayCommand]
    private async void Count()
    {
        _count++;
        if (_count == 1)
            Text = "Clicked 1 time";
        else if (_count > 1)
            Text = $"Clicked {_count} times";

        _screenReader.Announce(Text);
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
