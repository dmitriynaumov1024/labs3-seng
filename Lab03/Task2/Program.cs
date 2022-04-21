using System;
using System.Threading;
using System.Globalization;

class Program
{
    static void LoopUntilCtrlC ()
    {
        bool shouldContinue = true;
        Console.CancelKeyPress += (sender, args) => { 
            args.Cancel = true;
            shouldContinue = false; 
        };
        while (shouldContinue) {
            Thread.Sleep(1000);
        }
    }

    [STAThread]
    public static void Main (string[] args)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        EchoServer server = EchoServer.Instance;
        Console.Write("Starting...\n");
        server.RunAsync();
        Console.Write("Server is running on {0}:{1}\n", 
                      server.Address, server.Port);

        Console.Write("Press Ctrl+C to stop.\n");
        LoopUntilCtrlC();
        Console.Write("Stopping the server...\n");
        server.Stop();
        Console.Write("Server was stopped successfully.\n");
    }
}
