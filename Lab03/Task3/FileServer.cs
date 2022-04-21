using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class FileServer : EchoServer
{
    private static FileServer instance;
    public static FileServer Instance {
        get {
            if (instance == null) {
                var config = new FileServerConfig("fsconfig.xml");
                instance = new FileServer(config);
            }
            return instance;
        }
    }

    private string baseDir;

    protected FileServer(FileServerConfig config)
        : base(config.IpAddress, config.Port)
    {
        this.baseDir = config.Base;
        this.Serve = this.ServeImpl;
    }

    private void ServeImpl()
    {
        TcpClient client = this.AcceptTcpClient();
        Stream stream = client.GetStream();
        StreamReader reader = new StreamReader(stream);
        StreamWriter writer = new StreamWriter(stream);
        Console.Write("[FileServer.Serve]:\n");
        while (true) {
            string line = reader.ReadLine();
            if (line.Length == 0) {
                break;
            }
            if (line.ToLower().StartsWith("get")) {
                string path = line.Split(' ')[1];
                Console.Write("Path: {0}\n", path);
                writer.Write(Utils.PlainTextHttpHeader);
                writer.Write(Utils.GetFsEntriesText(path, this.baseDir));
                break;
            }
        }
        writer.Flush();
        client.Close();
    }
}
