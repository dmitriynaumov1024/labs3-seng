using System;
using System.IO;
using System.Xml;
using System.Text;

class FileServerConfig
{
    public string Base { get; private set; }
    public string IpAddress { get; private set; }
    public int Port { get; private set; }

    public FileServerConfig ()
    {
        this.Base = System.IO.Directory.GetCurrentDirectory().Replace('\\', '/');
        this.IpAddress = "127.0.0.1";
        this.Port = 8000;
    }

    public FileServerConfig (string configPath)
    {
        if (!File.Exists(configPath)) {
            new FileServerConfig();
            return;
        }
        XmlDocument doc = new XmlDocument();
        using (Stream source = new FileStream(configPath, FileMode.Open)) {
            doc.Load(source);
        }
        XmlElement configElement = doc.DocumentElement;
        this.Base = configElement["Base"].InnerText;
        this.IpAddress = configElement["IpAddress"].InnerText;
        this.Port = int.Parse(configElement["Port"].InnerText);
    }
}
