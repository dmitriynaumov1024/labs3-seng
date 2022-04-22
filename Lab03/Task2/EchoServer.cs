using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

class EchoServer : TcpListener
{
    private static string defaultAddress = "127.0.0.1";
    private static int defaultPort = 8000;
    private static EchoServer instance;
    public static EchoServer Instance {
        get {
            if (instance == null) {
                instance = new EchoServer(defaultAddress, defaultPort);
            }
            return instance;
        }
    }

    private bool shouldContinue = false;
    private int port;
    private string address;

    public int Port {
        get {
            return this.port;
        }
    }

    public string Address {
        get {
            return this.address;
        }
    }

    private EchoServer (string address, int port)
        : base (IPAddress.Parse(address), port)
    {
        this.address = address;
        this.port = port;
    }

    public Task RunAsync (int cycles = 0)
    {
        return Task.Run (() => {
            this.Run(cycles);
        });
    }

    public void Run (int cycles = 0)
    {
        if (!this.Active) {
            this.Start();
        }
        if (this.shouldContinue) {
            return;
        } 
        this.shouldContinue = true;
        if (cycles > 0) {
            for (int i=0; i<cycles && this.shouldContinue; i++) {
                this.Serve();
            }
        }
        else {
            while (this.shouldContinue) {
                this.Serve();
            }
        }
    }

    public void Stop ()
    {
        this.shouldContinue = false;
    }

    protected virtual void Serve()
    {
        TcpClient client = this.AcceptTcpClient();
        Stream stream = client.GetStream();
        StreamReader reader = new StreamReader(stream);
        StreamWriter writer = new StreamWriter(stream);
        Console.Write("[EchoServer.Serve]:\n");
        while (true) {
            string line = reader.ReadLine();
            if (line.Length == 0) {
                break;
            }
            Console.Write("{0}\r\n", line);
            writer.Write("{0}\r\n", line);
        }
        writer.Flush();
        client.Close();
    }

    ~EchoServer()
    {
        
    }
}
