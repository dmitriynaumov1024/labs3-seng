using System;
using System.Windows.Forms;

static class Program 
{
    [STAThread]
    public static void Main(string[] args)
    {
        var view = new MainView(new NumberModel());
        Application.Run(view);
    }
}
