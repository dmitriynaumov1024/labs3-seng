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
        var view = new MainView();
        Application.Run(view);
    }    
}
