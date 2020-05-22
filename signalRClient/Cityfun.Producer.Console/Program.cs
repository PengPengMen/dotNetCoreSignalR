using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Cityfun.Producer.Console
{
    class Program
    {
        private static HubConnection _hubConnection;

        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        static async Task MainAsync()
        {
            System.Console.Title = "SignalR-Publisher";

            await SetupSignalRHubAsync();

            System.Console.WriteLine("Connected to Hub");
            System.Console.WriteLine("Press ESC to stop Producer");
            do
            {
                while (!System.Console.KeyAvailable)
                {
                    System.Console.WriteLine("please input userId:");
                    string userId = System.Console.ReadLine();
                    System.Console.WriteLine("please input message:");
                    var message = System.Console.ReadLine();
                    await _hubConnection.SendAsync("SendMessageToUserId", userId, message);
                    System.Console.WriteLine($"SendAsync to Hub:{message}");
                }
            }
            while (System.Console.ReadKey(true).Key != ConsoleKey.Escape);

            await _hubConnection.DisposeAsync();
        }

        public static async Task SetupSignalRHubAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                 .WithUrl("https://localhost:44335/chathub/")
                 .Build();
            await _hubConnection.StartAsync();
        }
    }
}
