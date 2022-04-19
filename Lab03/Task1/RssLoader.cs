using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Text;
using System.Threading.Tasks;

class RssLoader
{
    // Singleton
    //
    private static RssLoader instance;

    public static RssLoader Instance {
        get {
            if (instance == null) {
                instance = new RssLoader();
            }
            return instance;
        }
    }

    // Private fields
    //
    private ILogger Log;

    // Constructor
    //
    private RssLoader()
    {
        this.Log = FileLogger.Instance;
    }

    // Methods
    //
    public Task<string> GetTextAsync(string urlString) 
    {
        return Task.Run(() => GetText(urlString));
    }

    public string GetText (string urlString) 
    {
        urlString = urlString.Trim();
        Uri address;
        Log.Write("");
        Log.Write("GetText: Starting...");
        try {
            address = new Uri(urlString, UriKind.Absolute);
        }
        catch (Exception ex) {
            Log.Write("GetText: Invalid Uri: {0}", urlString);
            return "Exception: Invalid Uri: " + urlString;
        }
        Log.Write("GetText: Valid Uri: {0}", urlString);
        Log.Write("GetText: Creating WebRequest...");
        try {
            HttpWebRequest request = WebRequest.CreateHttp(address);
            request.Proxy = null;
            Log.Write("GetText: Sending Request...");
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Log.Write("GetText: Parsing Response...");
            string result = ParseRssFrom(response.GetResponseStream());
            Log.Write("GetText: Done Parsing. Returning...");
            response.Close();
            return result;
        }
        catch (Exception ex) {
            return "Exception: Can not get anything from: " + urlString;
        }
    }

    string ParseRssFrom (Stream source)
    {
        XmlDocument document = new XmlDocument();
        string chunk = source.ToString();
        if (chunk.Length > 400) chunk = chunk.Substring(0, 400);
        try {
            Log.Write("ParseRss: Creating XmlDocument...");
            document.Load(XmlReader.Create(source));
        }
        catch (Exception ex) {
            string result = String.Format("Exception: That was not Xml:\r\n{0}...", chunk);
            Log.Write("ParseRss: {0}", result);
            return result;
        }
        finally {
            source.Close();
        }
        StringBuilder sb = new StringBuilder();
        var nodes = document.DocumentElement.SelectNodes("/rss/channel/item");
        int nodeCount = 0;
        foreach (object xmlNodeObj in nodes) {
            nodeCount++;
            XmlNode node = xmlNodeObj as XmlNode;
            string title = Utils.RemoveXmlTags(node["title"].InnerText),
                   text = Utils.RemoveXmlTags(node["description"].InnerText),
                   pubDate = node["pubDate"].InnerText;
            sb.Append(title);
            sb.Append("\r\n- ");
            sb.Append(pubDate);
            sb.Append(" -\r\n");
            sb.Append(text);
            sb.Append("\r\n\r\n\r\n");
        }
        if (nodeCount == 0) {
            string result = String.Format("Exception: Can not get anything from:\r\n{0}...", chunk);
            Log.Write("ParseRss: {0}", result);
            return result;
        }
        Log.Write("ParseRss: Done. Total {0} entries.", nodeCount);
        return sb.ToString();
    }
}
