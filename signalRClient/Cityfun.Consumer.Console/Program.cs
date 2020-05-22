using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Cityfun.Consumer.Console
{
    class Program
    {
        private static HubConnection _hubConnection;

        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        static async Task MainAsync()
        {
            System.Console.Title = "SignalR-Subscrber";
            await SetupSignalRHubAsync();


            //_hubConnection.On<string>("sendLogin", (userIdTest) =>
            //{
            //    userIdTest = userId;
            //}); 
            //_hubConnection.On<string, string>("ReceiveMessageToUserId", (userIdTest, message) =>
            // {
            //     userIdTest = userId;
            //     System.Console.WriteLine($"Received Message: {message}");
            // });
            // _hubConnection.InvokeAsync("ReceiveMessageToUserId", message);
            _hubConnection.On<string>("ReceiveMessageToUserId", (message) =>
            {
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

            //await _hubConnection.DisposeAsync();
            await _hubConnection.SendAsync("UserDisconnected");
        }

        public static async Task SetupSignalRHubAsync()
        {
            System.Console.WriteLine("please input userId:");
            string userId = System.Console.ReadLine();
            _hubConnection = new HubConnectionBuilder()
                 .WithUrl("https://localhost:44335/chathub/")
                 .Build();
            await _hubConnection.StartAsync();
            //await _hubConnection.SendAsync("sendLogin", userId);
            await _hubConnection.SendAsync("UserStartConnectedAsync", userId);
        }
    }
}
