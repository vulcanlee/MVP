namespace PrismApp2.ViewModels;

public class SplashPageViewModel : BindableBase, INavigatedAware
{
    private int _count;

    public SplashPageViewModel( INavigationService navigationService)
    {
        this.navigationService = navigationService;
        //CountCommand = new DelegateCommand(OnCountCommandExecuted);
    }

    public string Title => "Main Page";

    private string _text = "Click me";
    private readonly INavigationService navigationService;

    public string Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }

    public DelegateCommand CountCommand { get; }

    public void OnNavigatedFrom(INavigationParameters parameters)
    {
    }

    public async void OnNavigatedTo(INavigationParameters parameters)
    {
        await Task.Delay(3000);

        navigationService.NavigateAsync("NavigationPage/MainPage");

    }

    //private async void OnCountCommandExecuted()
    //{
    //    _count++;
    //    if (_count == 1)
    //        Text = "Clicked 1 time";
    //    else if (_count > 1)
    //        Text = $"Clicked {_count} times";

    //    _screenReader.Announce(Text);

    //    navigationService.NavigateAsync("/NextPage");

    //    return;

    //    if (MediaPicker.Default.IsCaptureSupported)
    //    {
    //        try
    //        {

    //            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

    //            if (photo != null)
    //            {
    //                // save the file into local storage
    //                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

    //                using Stream sourceStream = await photo.OpenReadAsync();
    //                using FileStream localFileStream = File.OpenWrite(localFilePath);

    //                await sourceStream.CopyToAsync(localFileStream);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine($"{ex.Message}");
    //        }
    //    }
    //}
}
