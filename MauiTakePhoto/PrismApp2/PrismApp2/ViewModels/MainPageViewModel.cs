namespace PrismApp2.ViewModels;

public class MainPageViewModel : BindableBase
{
    private ISemanticScreenReader _screenReader { get; }
    private int _count;

    public MainPageViewModel(ISemanticScreenReader screenReader)
    {
        _screenReader = screenReader;
        CountCommand = new DelegateCommand(OnCountCommandExecuted);
    }

    public string Title => "Main Page";

    private string _text = "Click me";
    public string Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }

    public DelegateCommand CountCommand { get; }

    private async void OnCountCommandExecuted()
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
}
