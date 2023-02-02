using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalrClientMaui.ViewModels;

public partial class MainPageViewModel : ObservableObject, INavigatedAware
{
    #region Field Member
    private int _count;
    private readonly INavigationService navigationService;
    HubConnection connection;
    [ObservableProperty]
    string title = "Main Page";

    [ObservableProperty]
    string receiveMessage = "";

    [ObservableProperty]
    string sendMessage = "";
    #endregion

    #region Property Member
    #endregion

    #region Constructor
    public MainPageViewModel(INavigationService navigationService)
    {
        #region 建立與產生 SignalR Client 端的物件
        connection = new HubConnectionBuilder()
           .WithUrl("https://localhost:7181/myChatHub")
           .Build();
        #endregion

        this.navigationService = navigationService;
    }
    #endregion

    #region Method Member
    #region Command Method
    [RelayCommand]
    private async Task Send()
    {
        await SendMessageToServer();
    }
    #endregion

    #region Navigation Event
    public void OnNavigatedFrom(INavigationParameters parameters)
    {
    }

    public async void OnNavigatedTo(INavigationParameters parameters)
    {
        BindingEvent();

        await StartSignalRConnectionAsync();
    }

    #endregion

    #region Other Method
    private async Task SendMessageToServer()
    {
        #region 開始對 SignalR 伺服器送出訊息
        try
        {
            await connection.InvokeAsync("SendMessage",
                "Vulcan", SendMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        #endregion

        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(500);
        }
    }

    private async Task StartSignalRConnectionAsync()
    {
        #region 啟動與後端 SignalR 伺服器進行連線
        try
        {
            await connection.StartAsync();
            Console.WriteLine("Connection started");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        #endregion
    }

    private void BindingEvent()
    {
        #region 綁定相關事件
        #region 網路發生異常，導致與後端 SignalR 伺服器連線中斷時機，將會觸發此事件
        connection.Closed += async (error) =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            try
            {
                await connection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            await MainThread.InvokeOnMainThreadAsync(async () =>
             {
             });
        };
        #endregion

        #region 綁定有特定訊息要被發送出來的時機，將會觸發此事件
        connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            var newMessage = $"{user}: {message}";
            if (string.IsNullOrEmpty(ReceiveMessage))
            {
                ReceiveMessage = newMessage;
            }
            else
            {
                ReceiveMessage += $"{Environment.NewLine}{newMessage}";
            }
            Console.WriteLine(newMessage);
        });
        #endregion
        #endregion
    }
    #endregion
    #endregion
}
