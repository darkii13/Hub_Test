
using System;
using Microsoft.ServiceBus.Messaging;

namespace SimpleEventProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            string eventHubConnectionString = "Endpoint=sb://eventhubdarek.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=XeJKqq3gA0Fs36qPxC47YqX/4B+81JSGzuyrZAjdLZc=";
            string eventHubName = "darekhub";
            string storageAccountName = "storagedarek";
            string storageAccountKey = "ePAtgqCTKJklEH9UHkTgkUYksG+sIRvx5aDWdu07qZIGN//METdHyb7zvHBMT3bWfT2ldbyGp2VUCnN8NbdTaw==";
            string storageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", storageAccountName, storageAccountKey);

            string eventProcessorHostName = Guid.NewGuid().ToString();
            EventProcessorHost eventProcessorHost = new EventProcessorHost(eventProcessorHostName, eventHubName, EventHubConsumerGroup.DefaultGroupName, eventHubConnectionString, storageConnectionString);
            Console.WriteLine("Registering EventProcessor...");
            var options = new EventProcessorOptions();
            options.ExceptionReceived += (sender, e) => { Console.WriteLine(e.Exception); };
            eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>(options).Wait();

            Console.WriteLine("Receiving. Press enter key to stop worker.");
            Console.ReadLine();
            eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }
    }
}