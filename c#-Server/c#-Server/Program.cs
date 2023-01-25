using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
namespace Fleck.Samples.ConsoleApp
{

    class Server
    {
        static void Main()
        {
            FleckLog.Level = LogLevel.Debug;
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer("ws://0.0.0.0:3000");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Open!");
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("Close!");
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    Console.WriteLine(message);
                    switch (message.Split(':')[0])
                    {
                        case "OnSubmit":
                            Console.WriteLine("Mi è arrivato un OnSubmit");
                            allSockets.ToList().ForEach(s => s.Send("OnSubmitResponse:" + message.Split(':')[1]));
                            break;
                        case "OnAutoLogin":
                            Console.WriteLine("Mi è arrivato un OnAutoLogin");
                            allSockets.ToList().ForEach(s => s.Send("OnAutoLoginResponse:" + message.Split(':')[1]));
                            break;
                        default:
                            break;

                    }
                };
            });


            var input = Console.ReadLine();
            while (input != "exit")
            {
                foreach (var socket in allSockets.ToList())
                {
                    socket.Send(input);
                }
                input = Console.ReadLine();
            }

        }
    }
}