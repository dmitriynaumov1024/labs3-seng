using System;
using System.Threading;
using System.Globalization;
using System.Windows.Forms;

class Program
{
    [STAThread]
    static void Main()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var rss = RssLoader.Instance;
        var view = new MainView(rss);
        Application.Run(view);
    }    
}
