using Microsoft.AspNetCore.SignalR.Client;

namespace SignalrClientConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            #region 建立與產生 SignalR Client 端的物件
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7181/myChatHub")
                .Build();
            #endregion

            #region 綁定相關事件
            #region 網路發生異常，導致與後端 SignalR 伺服器連線中斷時機，將會觸發此事件
            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
            #endregion

            #region 綁定有特定訊息要被發送出來的時機，將會觸發此事件
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var newMessage = $"{user}: {message}";
                Console.WriteLine(newMessage);
            });
            #endregion
            #endregion

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

            #region 開始對 SignalR 伺服器送出訊息
            try
            {
                await connection.InvokeAsync("SendMessage",
                    "Vulcan", "Hello everyone");
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
    }
}