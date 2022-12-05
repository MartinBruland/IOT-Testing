using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs.Consumer;
using CommandLine;

namespace ReadD2cMessages
{

    internal class Program
    {
        public static async Task Main(string[] args)
        {
            Parameters parameters = null;
            // Parse application parameters
            ParserResult<Parameters> result = Parser.Default.ParseArguments<Parameters>(args)
                .WithParsed(parsedParams => parameters = parsedParams)
                .WithNotParsed(errors => Environment.Exit(1));

            // Either the connection string must be supplied, or the set of endpoint, name, and shared access key must be.
            if (string.IsNullOrWhiteSpace(parameters.EventHubConnectionString)
                && (string.IsNullOrWhiteSpace(parameters.EventHubCompatibleEndpoint)
                    || string.IsNullOrWhiteSpace(parameters.EventHubName)
                    || string.IsNullOrWhiteSpace(parameters.SharedAccessKey)))
            {
                Console.WriteLine(CommandLine.Text.HelpText.AutoBuild(result, null, null));
                Environment.Exit(1);
            }

            Console.WriteLine("IoT Hub Quickstarts - Read device to cloud messages. Ctrl-C to exit.\n");

            // Set up a way for the user to gracefully shutdown
            using var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                cts.Cancel();
                Console.WriteLine("Exiting...");
            };

            // Run the sample
            await ReceiveMessagesFromDeviceAsync(parameters, cts.Token);

            Console.WriteLine("Cloud message reader finished.");
        }

        private static async Task ReceiveMessagesFromDeviceAsync(Parameters parameters, CancellationToken ct)
        {
            string connectionString = parameters.GetEventHubConnectionString();

            await using var consumer = new EventHubConsumerClient(
                EventHubConsumerClient.DefaultConsumerGroupName,
                connectionString,
                parameters.EventHubName);

            Console.WriteLine("Listening for messages on all partitions.");

            try
            {
               await foreach (PartitionEvent partitionEvent in consumer.ReadEventsAsync(ct))
                {
                    Console.WriteLine($"\nMessage received on partition {partitionEvent.Partition.PartitionId}:");

                    string data = Encoding.UTF8.GetString(partitionEvent.Data.Body.ToArray());
                    Console.WriteLine($"\tMessage body: {data}");

                    Console.WriteLine("\tApplication properties (set by device):");
                    foreach (KeyValuePair<string, object> prop in partitionEvent.Data.Properties)
                    {
                        PrintProperties(prop);
                    }

                    Console.WriteLine("\tSystem properties (set by IoT hub):");
                    foreach (KeyValuePair<string, object> prop in partitionEvent.Data.SystemProperties)
                    {
                        PrintProperties(prop);
                    }
                }
            }
            catch (TaskCanceledException)
            {
            }
        }

        private static void PrintProperties(KeyValuePair<string, object> prop)
        {
            string propValue = prop.Value is DateTime time
                ? time.ToString("O") // using a built-in date format here that includes milliseconds
                : prop.Value.ToString();

            Console.WriteLine($"\t\t{prop.Key}: {propValue}");
        }
    }
}
