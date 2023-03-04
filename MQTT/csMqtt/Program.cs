using MQTTnet.Client.Options;
using MQTTnet.Client;
using MQTTnet;
using System.Text;
using MQTTnet.Client.Subscribing;

namespace csMqttClient;

internal class Program
{
    private static IMqttClient _mqttClient;

    static async Task Main(string[] args)
    {
        // Create client (這裡使用 MQTTNET 套件，走的協定為 TCP 1883
        _mqttClient = new MqttFactory().CreateMqttClient();
        var options = new MqttClientOptionsBuilder()
            .WithClientId("MqttClient")
            .WithTcpServer("test.mosquitto.org", 1883)
            .Build();

        // When client connected to the server
        _mqttClient.UseConnectedHandler(async e =>
        {
            // Subscribe to a topic
            MqttClientSubscribeResult subResult =
            await _mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder()
            .WithTopicFilter("mqttServerTopic")
            .Build());
            // Sen a test message to the server
            Console.WriteLine($"> Send a test message to the server");
            // PublishMessage("從 .NET Core 使用 1883 埠送出的訊息 (Hello)");
        });

        // When client received a message from server
        _mqttClient.UseApplicationMessageReceivedHandler(e =>
        {
            Console.WriteLine($"> When client received a message from server");
            Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
        });

        // Connect ot server
        await _mqttClient.ConnectAsync(options, CancellationToken.None);

        while (true)
        {
            PublishMessage("從 .NET Core 使用 1883 埠送出的訊息 (Hello)");

            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Q)
            {
                break;
            }
        }
    }

    private static async void PublishMessage(string message)
    {
        // Create mqttMessage
        var mqttMessage = new MqttApplicationMessageBuilder()
                            .WithTopic("mqttServerTopic")
                            .WithPayload(message)
                            .WithExactlyOnceQoS()
                            .Build();

        // Publish the message asynchronously
        await _mqttClient.PublishAsync(mqttMessage, CancellationToken.None);
    }
}
