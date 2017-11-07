using System.Threading;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Text;

namespace Hub
{
    class Program
    {
        static string eventHubName = "darekhub";
        static string connectionString = "Endpoint=sb://eventhubdarek.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=XeJKqq3gA0Fs36qPxC47YqX/4B+81JSGzuyrZAjdLZc=";

        static void Main(string[] args)
        {
            Console.WriteLine("Press Ctrl-C to stop the sender process");
            Console.WriteLine("Press Enter to start now");
            Console.ReadLine();
            SendingRandomMessages();
        }

        static void SendingRandomMessages()
        {
            var eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);
            while (true)
            {
                try
                {
                    var message = Guid.NewGuid().ToString() + " Darek z Data = " + DateTimeOffset.Now.ToString();
                    Console.WriteLine("{0} > Sending message from DAREK: {1}", DateTime.Now, message);
                    eventHubClient.Send(new EventData(Encoding.UTF8.GetBytes(message)));
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} > Exception: {1}", DateTime.Now, exception.Message);
                    Console.ResetColor();
                }

                Thread.Sleep(5000);
            }
        }
    }
}
