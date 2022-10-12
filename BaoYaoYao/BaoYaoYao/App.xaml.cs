namespace BaoYaoYao;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Light;
        Application.Current.RequestedThemeChanged += (s, a) =>
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        };
    }
}
