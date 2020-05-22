using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SignalRClient
{
    class Program
    {
        private static HubConnection _hubConnection;

        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        static async Task MainAsync()
        {
            System.Console.Title = "SignalR-Subscrber";

            await SetupSignalRHubAsync();

            _hubConnection.On<string, string>("ReceiveMessageToClient", (userId, message) =>
            {
                userId = "123456";
                System.Console.WriteLine($"Received userId: {userId}");
                System.Console.WriteLine($"Received Message: {message}");
            });

            //_hubConnection.On<string>("ReceiveMessage", (message) =>
            //{
            //    System.Console.WriteLine($"Received Message: {message}");
            //});

            System.Console.WriteLine("Connected to Hub");
            System.Console.WriteLine("Press ESC to stop Consumer");
            do
            {

            }
            while (System.Console.ReadKey(true).Key != ConsoleKey.Escape);

            await _hubConnection.DisposeAsync();
        }

        public static async Task SetupSignalRHubAsync()
        {
            _hubConnection = new Hub
                 .WithUrl("http://localhost:5000/pwczfHub")
                 .Build();

            await _hubConnection.StartAsync();
        }

    }
}
