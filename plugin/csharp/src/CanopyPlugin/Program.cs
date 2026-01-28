using System;
using System.Threading;
using System.Threading.Tasks;

namespace CanopyPlugin
{
    public class Program
    {
        private const string PlaygroundVersion = "v0.1.0";

        public static async Task Main(string[] args)
        {
            Console.WriteLine($"Canopy Plugin Playground {PlaygroundVersion} (C#) - PABLO WAS HERE");

            var config = Config.Default();
            Console.WriteLine($"  Chain ID: {config.ChainId}");
            Console.WriteLine($"  Data Directory: {config.DataDirPath}");

            using var plugin = new Plugin(config);
            await plugin.StartAsync();

            Console.WriteLine("Plugin started - waiting for FSM requests...");

            // wait for shutdown signal
            using var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                Console.WriteLine("Received shutdown signal");
                cts.Cancel();
            };

            try
            {
                await Task.Delay(Timeout.Infinite, cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Plugin shut down gracefully");
            }
        }
    }
}
